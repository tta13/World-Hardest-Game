using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    public GameObject player;
    public GameObject[] enemies;
    public GameObject[] goalEnemies;
    public GameObject[] allEnemies;
    public GameObject goldenBall;
    public GameObject goal;
    public GameObject cam;
    public GameObject gamePanel;
    public GameObject winPanel;
    public GameObject pauseButton;
    public GameObject pausePanel;
    public float speedUp = 0.5f;
    
    public Vector2 initialEvenEnemyPosition = new Vector2(-3.5f, 2.0f);
    public Vector2 initialOddEnemyPosition = new Vector2(3.5f, 1.0f);
    public Vector2 initialOddGoalEnemyPosition = new Vector2(7.6f, 0.0f);
    public Vector2 initialEvenGoalEnemyPosition = new Vector2(5.5f, 2.0f);

    private bool gotBall = false;
    private float interpolationPeriod = 2.0f;
    private float timer = 0.0f;

    void Awake() 
    {
        if (instance == null)
            instance = this;
        Time.timeScale = 1f;
        goal.SetActive(false);
    }

    void Start()
    {
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

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if(timer >= interpolationPeriod && gotBall)
        {
            timer = 0.0f;
            StartCoroutine(CameraShake(0.25f));
        }
    }

    public void HitEnemy()
    {
        //Time.timeScale = 0f;
        FindObjectOfType<AudioManager>().PlayAudio("PlayerDeath");
        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        yield return new WaitForSecondsRealtime(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void BallCatched()
    {
        FindObjectOfType<AudioManager>().PlayAudio("Catch");
        goldenBall.GetComponent<SpriteRenderer>().enabled = false;
        goldenBall.GetComponent<CircleCollider2D>().enabled = false;
        goal.SetActive(true);
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allEnemies)
        {
            Debug.Log("Speeding Up Enemies");
            enemy.GetComponent<EnemyScript>().enemySpeed += enemy.GetComponent<EnemyScript>().enemySpeed * speedUp;
        }
        player.GetComponent<PlayerScript>().playerSpeed += player.GetComponent<PlayerScript>().playerSpeed * (speedUp + 0.1f);
        Debug.Log("Congratulations, you catched the Golden Ball, now, run back to your base!!");
        StartCoroutine(CameraShake(0.4f));
        gotBall = true;
    }

    public void PlayerWon()
    {
        FindObjectOfType<AudioManager>().PlayAudio("Win");
        Time.timeScale = 0f;
        gamePanel.SetActive(false);
        winPanel.SetActive(true);
    }

    public void CallRestart()
    {
        StartCoroutine("WinRestart");
    }

    IEnumerator WinRestart()
    {
        yield return new WaitForSecondsRealtime(0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    IEnumerator CameraShake(float duration)
    {
        FindObjectOfType<AudioManager>().PlayAudio("Explosion");
        Vector3 originalPos = cam.transform.localPosition;
        float timeElapsed = 0.0f;
        while(timeElapsed < duration)
        {
            float x = Random.Range(-1f, 1f)*0.4f;
            float y = Random.Range(-1f, 1f)*0.4f;
            cam.transform.localPosition = new Vector3(x, y, originalPos.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        cam.transform.localPosition = originalPos;
    }

    public void PauseGame()
    {
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);
        StartCoroutine(PauseMenuStart());
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(PauseMenuEnd());
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    IEnumerator PauseMenuStart()
    {
        Animator a = pausePanel.GetComponent<Animator>();
        a.SetBool("Open", true);
        yield return new WaitForSeconds(0.833f);
        Time.timeScale = 0f;
    }

    IEnumerator PauseMenuEnd()
    {
        Animator a = pausePanel.GetComponent<Animator>();
        a.SetBool("Open", false);
        yield return new WaitForSeconds(0.833f);
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }
}
