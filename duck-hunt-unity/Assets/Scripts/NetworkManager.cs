
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using SDKConfiguration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;
using TMPro;

public class NetworkManager : MonoBehaviour
{
    public FirebaseAuth auth;
    public FirebaseUser CurrentUser;

    public bool signedIn;
    public DatabaseReference DBref;

    public TextMeshProUGUI CoinsText;
    public static NetworkManager instance;

    public int coins;
    public void Awake()
    {
        instance = this;
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        DBref = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Debug.Log("Auth State Changed");
        if (auth.CurrentUser != CurrentUser || CurrentUser == null)
        {
            signedIn = CurrentUser != auth.CurrentUser && auth.CurrentUser != null;
            CurrentUser = auth.CurrentUser;
            if (signedIn)
            {
                InvokeRepeating("UpdateCoins", 1, 5);
            }
            else
            {
                InvokeRepeating("UpdateCoins", 1, 5);
               // CoinsText.text = "Not Signed in";
               // coins = 0;
            }
        }
    }


    void UpdateCoins()
    {
        LoadUserData("Coins", (coins) => { CoinsText.text = coins; this.coins = int.Parse(coins); });
    }

    public void Login()
    {
        auth.SignInWithCustomTokenAsync(PlayerPrefs.GetString("Account")).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCustomTokenAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCustomTokenAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    public void SaveUserData(string name, string value)
    {
        var DBTask = DBref.Child("DuckHuntUsers").Child(PlayerPrefs.GetString("Account")).Child(name).SetValueAsync(value);
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(DBTask.Exception);
        }
        else
        {
            Debug.Log("Saved Data: " + name + " Amount " + value.ToString());
        }
    }
    public void LoadUserData(string name, Action<string> OnReturn)
    {
        FirebaseDatabase dbInstance = FirebaseDatabase.DefaultInstance;
        dbInstance.GetReference("DuckHuntUsers").Child(PlayerPrefs.GetString("Account")).Child(name).GetValueAsync().ContinueWithOnMainThread(DBTask =>
        {

            if (DBTask.IsFaulted)
            {
                Debug.LogWarning(DBTask.Exception);
                Debug.Log("Exception");
            }

            else if (DBTask.IsCompleted)
            {
                DataSnapshot snapshot = DBTask.Result;

                if (snapshot.Value == null)
                {
                    SaveUserData(name, 500.ToString());
                    OnReturn(0.ToString());
                }
                else
                {
                    var val = (snapshot.Value);
                    string _val = val.ToString();
                    OnReturn(_val);
                }
            }
        });
    }

}
