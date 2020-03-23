using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    public GameObject player;
    public GameObject[] allEnemies;
    public GameObject goldenBall;
    public GameObject goal;
    public GameObject cam;
    public GameObject gamePanel;
    public GameObject winPanel;
    public GameObject pauseButton;
    public GameObject pausePanel;
    public float speedUp = 0.5f;
   
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

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if(timer >= interpolationPeriod && gotBall)
        {
            timer = 0.0f;
            StartCoroutine(cam.GetComponent<CameraShaker>().Shake(0.25f, 0.4f));
        }
    }

    public void PlayerDied()
    {
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
            enemy.GetComponent<EnemyScript>().speed += enemy.GetComponent<EnemyScript>().speed * speedUp;
        }
        player.GetComponent<PlayerScript>().speed += player.GetComponent<PlayerScript>().speed * (speedUp + 0.1f);

        Debug.Log("You catched the Golden Ball, now, run back to your base!!");

        StartCoroutine(cam.GetComponent<CameraShaker>().Shake(0.4f, 0.5f));

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
