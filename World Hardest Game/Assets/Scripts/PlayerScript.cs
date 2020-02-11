﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float playerSpeed = 2.0f;

    // Update is called once per frame
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
}
