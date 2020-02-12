﻿using System.Collections;
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

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * playerSpeed * Time.deltaTime;
            //Debug.Log("You clicked on W");
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * playerSpeed * Time.deltaTime;
            //Debug.Log("You clicked on A");
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * playerSpeed * Time.deltaTime;
            //Debug.Log("You clicked on S");
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * playerSpeed * Time.deltaTime;
            //Debug.Log("You clicked on D");
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Enemy")
        {
            Debug.Log("You hit an enemy");
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
