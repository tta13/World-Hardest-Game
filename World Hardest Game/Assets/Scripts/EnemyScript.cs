using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : CharacterScript
{
    [SerializeField]
    private bool moveLeft;

    public override void Init()
    {
        speed = 4f;
    }

    public override void Move()
    {
        if(moveLeft)
            transform.position += Vector3.left * speed * Time.fixedDeltaTime;
        else
            transform.position += Vector3.right * speed * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "BounceBall")
        {
            moveLeft = !moveLeft;
        }
    }

}
