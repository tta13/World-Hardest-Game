using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterScript : MonoBehaviour
{
    public float speed;
    
    void Start()
    {
        Init();
    }

    public virtual void Init()
    {

    }

    private void FixedUpdate()
    {
        Move();
    }

    public abstract void Move();
    
    public void SpeedUp(float speedUp)
    {
        this.speed *= (1f + speedUp);
    }
}
