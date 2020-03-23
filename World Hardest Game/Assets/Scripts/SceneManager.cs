using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    #region PublicFields
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
    #endregion

    #region PrivateFields
    private float speedUp = 0.5f;
    private bool gotBall = false;
    private float interpolationPeriod = 2.0f;
    private float timer = 0.0f;
    #endregion

    #region Monobehaviour
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
    #endregion

    #region GameManaging
    public void PlayerDied()
    {
        FindObjectOfType<AudioManager>().PlayAudio("PlayerDeath");
        StartCoroutine(Restart(1f));
    }


    public void BallCatched()
    {
        FindObjectOfType<AudioManager>().PlayAudio("Catch");

        goldenBall.SetActive(false);

        goal.SetActive(true);

        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allEnemies)
        {
            Debug.Log("Speeding Up Enemies");
            enemy.GetComponent<EnemyScript>().SpeedUp(speedUp);
        }
        player.GetComponent<PlayerScript>().SpeedUp(speedUp + 0.1f);

        Debug.Log("You catched the Golden Ball, now, run back to your base!!");

        StartCoroutine(cam.GetComponent<CameraShaker>().Shake(0.4f, 0.5f));

        gotBall = true;
    }

    public void PlayerWon()
    {
        FindObjectOfType<AudioManager>().PlayAudio("Win");
        Time.timeScale = 0f;
        gamePanel.SetActive(false);
        pauseButton.SetActive(false);
        winPanel.SetActive(true);
    }

    IEnumerator Restart(float sleepTime)
    {
        yield return new WaitForSecondsRealtime(sleepTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    #endregion

    #region UIMethods
    public void CallRestart()
    {
        StartCoroutine(Restart(0f));
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
    #endregion
}
