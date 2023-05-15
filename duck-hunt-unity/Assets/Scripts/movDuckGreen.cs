using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movDuckGreen : MonoBehaviour
{
    private Vector3 leftTopCameraBorder;
    private Vector3 rightBottomCameraBorder;
    private Vector3 siz;
    // Start is called before the first frame update
    void Start()
    {
        rightBottomCameraBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        leftTopCameraBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));



        siz.x = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;

        transform.position = new Vector3(leftTopCameraBorder.x - (siz.x / 2), transform.position.y, transform.position.z);
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(2, 5), 0);
    }

    // Update is called once per frame
    void Update()
    {
        siz.x = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        if (transform.position.x - (siz.x / 2) > rightBottomCameraBorder.x)
        {
            Destroy(gameObject);
        }
        siz.y = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        if (transform.position.y - (siz.y / 2) < rightBottomCameraBorder.y)
        {
            Destroy(gameObject);
        }

    }
}
