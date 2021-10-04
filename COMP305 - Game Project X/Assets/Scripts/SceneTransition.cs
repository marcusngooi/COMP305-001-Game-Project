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
            LoadNextLevel();
        }

        else if (other.gameObject.CompareTag("FinishLine"))
        {
            LoadMainMenu();
        }
    }

    private void LoadNextLevel()
    { 
        // take the current scene and go on next scene based on build's scene order
       StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1)); 
    } 

    // for main menu
    private void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator LoadLevel(int levelindex)
    {
        // play the animation
        transition.SetTrigger("Start");
        // wait
        yield return new WaitForSeconds(transitionTime);

        // load scene
        SceneManager.LoadScene(levelindex);
    }
}
