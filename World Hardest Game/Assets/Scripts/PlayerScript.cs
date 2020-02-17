using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float playerSpeed = 2.0f;
    SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * playerSpeed * Time.fixedDeltaTime;
            //Debug.Log("You clicked on W");
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * playerSpeed * Time.fixedDeltaTime;
            //Debug.Log("You clicked on A");
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * playerSpeed * Time.fixedDeltaTime;
            //Debug.Log("You clicked on S");
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * playerSpeed * Time.fixedDeltaTime;
            //Debug.Log("You clicked on D");
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Enemy")
        {
            Debug.Log("You hit an enemy");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine("FadeOut");
            SceneManager.instance.HitEnemy();
        }
        else if(target.tag == "Goal")
        {
            Debug.Log("You have reached your goal.");
            SceneManager.instance.PlayerWon();
        }
        else if(target.tag == "GoldenBall")
        {
            SceneManager.instance.BallCatched();
        }
    }

    IEnumerator FadeOut()
    {
        Debug.Log("Fading Out");
        for(float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color cor = rend.material.color;
            cor.a = f;
            rend.material.color = cor;
            yield return new WaitForSeconds(0.05f);
        }
    }

}
