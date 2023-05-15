using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreAffiche : MonoBehaviour
{
    public manager scriptManager;
    private Sprite spritetransisiton;


    public Sprite[] spriteTab = new Sprite[] { };
    private string score = "";


    // Update is called once per frame
    void Update()
    {
        if (scriptManager.getScore().ToString() != score)
        {
            score = scriptManager.getScore().ToString();
            Debug.Log(score + " affichage");
            switch (score.Length)
            {
                case 1:
                    spritetransisiton = spriteTab[int.Parse(score)];
                    GameObject.FindGameObjectsWithTag("score3")[0].GetComponent<SpriteRenderer>().sprite = spritetransisiton;
                    break;
                case 2:
                    spritetransisiton = spriteTab[int.Parse(score[1].ToString())];
                    GameObject.FindGameObjectsWithTag("score3")[0].GetComponent<SpriteRenderer>().sprite = spritetransisiton;
                    spritetransisiton = spriteTab[int.Parse(score[0].ToString())];
                    GameObject.FindGameObjectsWithTag("score2")[0].GetComponent<SpriteRenderer>().sprite = spritetransisiton;
                    break;
                case 3:
                    spritetransisiton = spriteTab[int.Parse(score[2].ToString())];
                    GameObject.FindGameObjectsWithTag("score3")[0].GetComponent<SpriteRenderer>().sprite = spritetransisiton;
                    spritetransisiton = spriteTab[int.Parse(score[1].ToString())];
                    GameObject.FindGameObjectsWithTag("score2")[0].GetComponent<SpriteRenderer>().sprite = spritetransisiton;
                    spritetransisiton = spriteTab[int.Parse(score[0].ToString())];
                    GameObject.FindGameObjectsWithTag("score1")[0].GetComponent<SpriteRenderer>().sprite = spritetransisiton;
                    break;
            }
        }
    }
}
