using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    // variables
    [SerializeField] private Animator transition;

    [SerializeField] private float transitionTime = 2f;

    [SerializeField] private Rigidbody2D player;

    // called during the trigger 

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadNextLevel();
            Debug.Log("hello");
        }
    }
    public void LoadNextLevel()
     {
         StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
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
