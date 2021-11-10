using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    Animator[] animators;
    public SceneTransition transition;
    public SettingsMenuController settings;
    public LevelMenuController level;
    public RectTransform title;
    public void ButtonPressed(int index)
    {
        animators = GetComponentsInChildren<Animator>();
        foreach(Animator animator in animators)
        {
            animator.enabled = false;
        }
        StartCoroutine(ChangeMenu(index));
    }
    private void OnEnable()
    {
        animators = GetComponentsInChildren<Animator>();
        foreach (Animator animator in animators)
        {
            animator.enabled = true;
        }
        title.localScale = new Vector3(1,1,1);

    }

    private IEnumerator ChangeMenu(int index)
    {
        Animation anim = this.GetComponent<Animation>();
        anim.Play();
        yield return new WaitForSeconds(0.90f);

        
        if(index == 0)
        {
            //play
            transition.PlayGame();
        }else if(index == 1)
        {
            //level select
            level.LevelMenu();
            this.gameObject.SetActive(false);
        }
        else if(index == 2)
        {
            //settings
            settings.SettingsMenu();
            this.gameObject.SetActive(false);

        }else
        {
            //quit
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
