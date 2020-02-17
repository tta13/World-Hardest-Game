using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float enemySpeed = 4.0f;
    [SerializeField]
    private bool moveLeft;

    void FixedUpdate()
    {
        if(moveLeft)
        {
            transform.position += Vector3.left * enemySpeed * Time.fixedDeltaTime;
        }
        else
        {
            transform.position += Vector3.right * enemySpeed * Time.fixedDeltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "BounceBall")
        {
            moveLeft = !moveLeft;
        }
    }

}
