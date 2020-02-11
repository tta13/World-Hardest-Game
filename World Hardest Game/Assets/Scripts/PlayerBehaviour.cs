using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Vector2 initialPosition = new Vector2(-6.0f, -2.0f);
    float playerSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = initialPosition;
    }

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
}
