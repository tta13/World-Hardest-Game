using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : CharacterScript
{
    [SerializeField]
    private float minX, maxX, minY, maxY;
    
    private float horizontalMove, verticalMove;

    private SpriteRenderer rend;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public override void Init()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        transform.position = new Vector2(x, y);
    }

    void GetInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
    }

    private void Update()
    {
        GetInput();
    }

    public override void Move()
    {
        transform.position += Vector3.right * speed * horizontalMove * Time.fixedDeltaTime;
        transform.position += Vector3.up * speed * verticalMove * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Enemy")
        {
            Debug.Log("You hit an enemy");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine("FadeOut");
            SceneManager.instance.PlayerDied();
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
