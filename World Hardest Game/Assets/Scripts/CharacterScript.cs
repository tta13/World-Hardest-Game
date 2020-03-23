using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterScript : MonoBehaviour
{
    public float speed;
    
    [SerializeField]
    private float x, y;

    void Start()
    {
        SetInitialPos();
    }

    public virtual void SetInitialPos()
    {
        gameObject.transform.position = new Vector2(x, y);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public abstract void Move();
}
