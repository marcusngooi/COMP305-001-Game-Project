using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

            Pause();
              
            }
        }
    public void Pause()
    {        
       pauseMenu.SetActive(true);
       Time.timeScale = 0f;
    }

    
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}
