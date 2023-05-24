using SDKConfiguration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;
using TMPro;
using FirebaseWebGL.Examples.Utils;
using FirebaseWebGL.Scripts.FirebaseBridge;
using FirebaseWebGL.Scripts.Objects;

public class NetworkManager : MonoBehaviour
{
   

    public bool signedIn;
    

    public TextMeshProUGUI CoinsText;
    public static NetworkManager instance;

    public int coins;
    public void Awake()
    {
        instance = this;
        InvokeRepeating("UpdateCoins", 1, 5);
    }


    void UpdateCoins()
    {
        LoadUserData("Coins");
    }

    public void SaveUserData(string name, string value)
    {
        FirebaseDatabase.PostJSON("DuckHuntUsers" + PlayerPrefs.GetString("Account") + name, value, gameObject.name, "OnSaved", "OnSavedFail");
    }

    void OnSaved(string info)
    {
        Debug.Log("Saved Data: " + info);
    }
    void OnSavedFail(string error)
    {
        Debug.LogWarning(error);
    }
    public void LoadUserData(string name)
    {
        FirebaseDatabase.GetJSON("DuckHuntUsers" + PlayerPrefs.GetString("Account") + name, gameObject.name, "OnLoaded", "OnLoadFailed");
    }

    void OnLoaded(string info)
    {
        if(info == null)
        {
            SaveUserData(name, 500.ToString());
        }
        else
        {
            CoinsText.text = info; this.coins = int.Parse(info);
            //OnReturn(_val);
        }
    }

    void OnLoadFailed(string error)
    {
        Debug.LogWarning(error);
    }

}
