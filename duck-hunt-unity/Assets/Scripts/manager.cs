using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{

    public AudioSource canardSound;
    public AudioSource touchSound;
    public Sprite spriteTouchBlue;
    public Sprite spriteTouchGreen;
    public Sprite spriteTouchRed;
    public Sprite spriteDieBlue;
    public Sprite spriteDieGreen;
    public Sprite spriteDieRed;
    public Sprite spriteRed;

    private Sprite vide;


    private int NumberOfDucksSpawned = 0;
    private int index;
    private bool red;

    public static int score;

    public float GameTimer;



    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.SetInt("score", 0);
        NumberOfDucksSpawned = 0;
        index = 1;
        red = false;
        GameTimer = 60;
    }

    // Update is called once per frame
    void Update()
    {
        GameTimer -= Time.deltaTime;
        if(GameTimer < 0)
        {
            PlayerPrefs.SetInt("score", score);
            SceneManager.LoadScene("MenuScene");
        }
        int OnScreenDucks = GameObject.FindGameObjectsWithTag("duckGreen").Length + GameObject.FindGameObjectsWithTag("duckRed").Length + GameObject.FindGameObjectsWithTag("duckBlue").Length;
        if (OnScreenDucks < 5 && Random.Range(1, 50) == 1)
        {
            if (Random.Range(1, 10) == 1)
            {
                canardSound.Play();
            }
            int randomDuck = Random.Range(1, 4);
            float randomNumber = Random.Range(-8, 43) / 10.0f;
            switch (randomDuck)
            {
                case 1:
                    GameObject gG = Instantiate(Resources.Load("duckGreen"), new Vector3(-12, randomNumber, 1), Quaternion.identity) as GameObject;
                    break;
                case 2:
                    GameObject gR = Instantiate(Resources.Load("duckRed"), new Vector3(-12, randomNumber, 1), Quaternion.identity) as GameObject;
                    break;
                case 3:
                    GameObject gB = Instantiate(Resources.Load("duckBlue"), new Vector3(-12, randomNumber, 1), Quaternion.identity) as GameObject;
                    break;
            }

          
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(PlayerPrefs.GetInt("score"));
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                hit.collider.gameObject.GetComponent<Animator>().enabled = false;
                switch (hit.collider.gameObject.tag)
                {
                    case "duckBlue":
                        score = score + 3;
                        //PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 3);
                        StartCoroutine(Timer(hit, spriteTouchBlue, spriteDieBlue));
                        break;
                    case "duckRed":
                        score = score + 5;
                        // PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 5);
                        StartCoroutine(Timer(hit, spriteTouchRed, spriteDieRed));


                        break;
                    case "duckGreen":
                        score = score + 1;
                        //PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 1);
                        StartCoroutine(Timer(hit, spriteTouchGreen, spriteDieGreen));
                        break;


                }

                //Debug.Log(hit.collider.gameObject.name);

            }
        }

        //StartCoroutine(Timer());



    }
    IEnumerator Timer(RaycastHit2D hit, Sprite touch, Sprite DeathSprite)
    {
        touchSound.Play();
        hit.collider.enabled = false;
        hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = touch;
        yield return new WaitForSeconds(0.5F);

        hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = DeathSprite;
        hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -3);
    }

    public int getScore()
    {
        return score;
    }

    public void setScore(int scoreChangement)
    {
        score = scoreChangement;
    }
}
