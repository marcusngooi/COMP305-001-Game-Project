using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    // variables
    [SerializeField] private Animator transition;

    [SerializeField] private float transitionTime = 2f;

    // called during the trigger 
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadScoreScreen();
        }

        else if (other.gameObject.CompareTag("FinishLine"))
        {
            LoadScoreScreen();
        }
    }

    public void LoadScoreScreen()
    {
        PlayerPrefs.SetInt("scene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("ScoreScreen");
    }
    public void LoadNextLevel()
    {
        // take the current scene and go on next scene based on build's scene order
       
        StartCoroutine(LoadLevel(PlayerPrefs.GetInt("scene")+1)); 
    } 

    // for main menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameOver()
    {
        PlayerPrefs.SetInt("scene", 0);
        SceneManager.LoadScene("Game_Over");
    }

    IEnumerator LoadLevel(int levelindex)
    {
        // play the animation
        transition.SetTrigger("Start");
        // wait
        yield return new WaitForSecondsRealtime(transitionTime);

        // load scene
        SceneManager.LoadScene(levelindex);
    }
}
