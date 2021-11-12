using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public EventSystem eventSystem;
    public void LevelMenu()
    {
        this.gameObject.SetActive(true);
        eventSystem.SetSelectedGameObject(GameObject.Find("Button"));
        this.GetComponent<Animation>().Play("levelmenuopen");
    }

    public void StartAnimationCoroutine()
    {
        StartCoroutine(CloseAnimation());
    }
    private IEnumerator CloseAnimation()
    {
        this.GetComponent<Animation>().Play("levelmenuclose");
        yield return new WaitForSeconds(0.9f);
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
