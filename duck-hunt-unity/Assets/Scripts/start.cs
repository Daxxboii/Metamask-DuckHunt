using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    public AudioSource musique;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.name == "start")
                {
                    StartCoroutine(Timer());
                }
                //hit.collider.attachedRigidbody.AddForce(Vector2.up);
            }
        }
    }
     IEnumerator Timer(){
        musique.Play();
        yield return new WaitForSeconds(0.5F);
        if (NetworkManager.instance.coins > 0) { 
            SceneManager.LoadScene("Main");
            NetworkManager.instance.LoadUserData("Coins", (value) => 
            {
                int _coins = int.Parse(value) - 20;
                NetworkManager.instance.SaveUserData("Coins",_coins.ToString());
            });
        
        }
     }
}
