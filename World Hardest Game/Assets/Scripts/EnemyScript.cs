using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float enemySpeed = 4.0f;
    [SerializeField]
    private bool moveLeft;

    void Update()
    {
        if(moveLeft)
        {
            transform.position += Vector3.left * enemySpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * enemySpeed * Time.deltaTime;
        }
    }
}
