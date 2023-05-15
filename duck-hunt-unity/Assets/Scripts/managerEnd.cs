using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerEnd : MonoBehaviour
{
    public AudioSource musique;
    private string score;
    private Sprite vide;
    public Sprite newHscore;

    public Sprite dogTwoDuck;
    private string highScore;
    public Sprite[] spriteTab = new Sprite[] { };
    // Start is called before the first frame update
    void Start()
    {
        
        musique.Play();
        score = PlayerPrefs.GetInt("score").ToString();
        switch (score.Length)
        {
            case 1:
                GameObject.FindGameObjectsWithTag("score3")[0].GetComponent<SpriteRenderer>().sprite = spriteTab[int.Parse(score)];
                break;
            case 2:
                GameObject.FindGameObjectsWithTag("score3")[0].GetComponent<SpriteRenderer>().sprite = spriteTab[int.Parse(score[1].ToString())];
                GameObject.FindGameObjectsWithTag("score2")[0].GetComponent<SpriteRenderer>().sprite = spriteTab[int.Parse(score[0].ToString())];
                break;
            case 3:
                GameObject.FindGameObjectsWithTag("score3")[0].GetComponent<SpriteRenderer>().sprite = spriteTab[int.Parse(score[2].ToString())];
                GameObject.FindGameObjectsWithTag("score2")[0].GetComponent<SpriteRenderer>().sprite = spriteTab[int.Parse(score[1].ToString())];
                GameObject.FindGameObjectsWithTag("score1")[0].GetComponent<SpriteRenderer>().sprite = spriteTab[int.Parse(score[0].ToString())];
                break;
        }
        if (PlayerPrefs.GetInt("score") > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("score"));
            GameObject.FindGameObjectsWithTag("newHS")[0].GetComponent<SpriteRenderer>().sprite = newHscore;
            GameObject.FindGameObjectsWithTag("dog")[0].GetComponent<SpriteRenderer>().sprite = dogTwoDuck;

        }
        highScore = PlayerPrefs.GetInt("HighScore").ToString();
        NetworkManager.instance.SaveUserData("HighScore", highScore);
        switch (highScore.Length)
        {
            case 1:
                GameObject.FindGameObjectsWithTag("HScore3")[0].GetComponent<SpriteRenderer>().sprite = spriteTab[int.Parse(highScore)];
                break;
            case 2:
                GameObject.FindGameObjectsWithTag("HScore3")[0].GetComponent<SpriteRenderer>().sprite = spriteTab[int.Parse(highScore[1].ToString())];
                GameObject.FindGameObjectsWithTag("HScore2")[0].GetComponent<SpriteRenderer>().sprite = spriteTab[int.Parse(highScore[0].ToString())];
                break;
            case 3:
                GameObject.FindGameObjectsWithTag("HScore3")[0].GetComponent<SpriteRenderer>().sprite = spriteTab[int.Parse(highScore[2].ToString())];
                GameObject.FindGameObjectsWithTag("HScore2")[0].GetComponent<SpriteRenderer>().sprite = spriteTab[int.Parse(highScore[1].ToString())];
                GameObject.FindGameObjectsWithTag("HScore1")[0].GetComponent<SpriteRenderer>().sprite = spriteTab[int.Parse(highScore[0].ToString())];
                break;
        }
    }
}
