using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public EventSystem eventSystem;
    public SceneTransition transition;
    public void LevelMenu()
    {
        this.gameObject.SetActive(true);
        eventSystem.SetSelectedGameObject(GameObject.Find("Button"));
        this.GetComponent<Animation>().Play("levelmenuopen");
    }

    public void LoadLevel(int level)
    {
        StartCoroutine(CloseAnimation(level));
    }
    public void StartAnimationCoroutine()
    {
        StartCoroutine(CloseAnimation(0));
    }
    private IEnumerator CloseAnimation(int level)
    {
        this.GetComponent<Animation>().Play("levelmenuclose");
        yield return new WaitForSeconds(0.9f);
        if(level != 0)
        {
            transition.PlayLevel(level);
        }else
        {
            this.gameObject.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}
