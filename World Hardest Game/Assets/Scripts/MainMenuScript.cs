using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.0f;

    public void PlayGame()
    {
        StartCoroutine(LoadLevel(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex);
    }

}
