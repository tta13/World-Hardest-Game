using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemies;
    public Vector2 initialPlayerPosition = new Vector2(-6.5f, -0.5f);
    public Vector2 initialEvenEnemyPosition = new Vector2(-3.5f, 2.0f);
    public Vector2 initialOddEnemyPosition = new Vector2(3.5f, 1.0f);
    public float playerSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = initialPlayerPosition;
        for(int i = 0; i<enemies.Length; i++)
        {
            if(i%2 == 0)
            {
                enemies[i].transform.position = initialEvenEnemyPosition;
                initialEvenEnemyPosition -= new Vector2(0.0f, 2.0f);
            }
            else
            {
                enemies[i].transform.position = initialOddEnemyPosition;
                initialOddEnemyPosition -= new Vector2(0.0f, 2.0f);
            }
        }
    }

}
