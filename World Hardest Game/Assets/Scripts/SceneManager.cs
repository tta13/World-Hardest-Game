using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemies;
    public GameObject[] goalEnemies;
    public Vector2 initialPlayerPosition = new Vector2(-6.5f, -0.5f);
    public Vector2 initialEvenEnemyPosition = new Vector2(-3.5f, 2.0f);
    public Vector2 initialOddEnemyPosition = new Vector2(3.5f, 1.0f);
    public Vector2 initialOddGoalEnemyPosition = new Vector2(7.5f, 0.0f);
    public Vector2 initialEvenGoalEnemyPosition = new Vector2(5.5f, 2.0f);

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

        for (int i = 0; i < goalEnemies.Length; i++)
        {
            if (i % 2 == 0)
            {
                goalEnemies[i].transform.position = initialEvenGoalEnemyPosition;
                initialEvenGoalEnemyPosition -= new Vector2(0.0f, 3.0f);
            }
            else
            {
                goalEnemies[i].transform.position = initialOddGoalEnemyPosition;
                initialOddGoalEnemyPosition -= new Vector2(0.0f, 3.0f);
            }
        }

    }

}
