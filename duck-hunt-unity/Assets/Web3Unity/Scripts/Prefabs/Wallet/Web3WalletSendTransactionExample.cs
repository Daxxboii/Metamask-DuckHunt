using UnityEngine;
using Web3Unity.Scripts.Library.Web3Wallet;

public class Web3WalletSendTransactionExample : MonoBehaviour
{
    async public void OnSendTransaction()
    {
        // https://chainlist.org/
        string chainId = "97"; 
        // account to send to
        string to = "0xdD4c825203f97984e7867F11eeCc813A036089D1";
        // value in wei
        string value = "12";
        // data OPTIONAL
        string data = "";
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        // send transaction
        string response = await Web3Wallet.SendTransaction(chainId, to, value, data, gasLimit, gasPrice);

        Debug.Log(response);

        if(response == "200 ok ")
        {
            NetworkManager.instance.SaveUserData("Coins", "200");
        }
    }
}
