using System.Runtime.InteropServices;
using System.Text;
using Nethereum.Signer;
using Nethereum.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Web3Unity.Scripts.Library.Web3Wallet;

public class WalletLogin : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void Web3Connect();

    [DllImport("__Internal")]
    private static extern string ConnectAccount();

    [DllImport("__Internal")]
    private static extern void SetConnectAccount(string value);

    private int expirationTime;
    private string account;

    ProjectConfigScriptableObject projectConfigSO = null;
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        projectConfigSO = (ProjectConfigScriptableObject)Resources.Load("ProjectConfigData", typeof(ScriptableObject));
        PlayerPrefs.SetString("ProjectID", projectConfigSO.ProjectID);
        PlayerPrefs.SetString("ChainID", projectConfigSO.ChainID);
        PlayerPrefs.SetString("Chain", projectConfigSO.Chain);
        PlayerPrefs.SetString("Network", projectConfigSO.Network);
        PlayerPrefs.SetString("RPC", projectConfigSO.RPC);

       /* // if remember me is checked, set the account to the saved account
        if (PlayerPrefs.HasKey("RememberMe") && PlayerPrefs.HasKey("Account"))
            if (PlayerPrefs.GetInt("RememberMe") == 1 && PlayerPrefs.GetString("Account") != "")
                // move to next scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);*/
    }
    public async void OnLogin()
    {
        Web3Connect();
        OnConnected();
    }
    async private void OnConnected()
    {
        account = ConnectAccount();
        while (account == "")
        {
            await new WaitForSeconds(1f);
            account = ConnectAccount();
        };
        // save account for next scene
        PlayerPrefs.SetString("Account", account);
        // reset login message
        SetConnectAccount("");
        // load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
   
}