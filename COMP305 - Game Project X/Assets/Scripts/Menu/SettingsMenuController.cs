using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public EventSystem eventSystem;
    public void SettingsMenu()
    {
        this.gameObject.SetActive(true);
        eventSystem.SetSelectedGameObject(GameObject.Find("VolumeSlider"));
        this.GetComponent<Animation>().Play("settingsmenuopen");
    }

    public void StartAnimationCoroutine()
    {
        StartCoroutine(CloseAnimation());
    }
    private IEnumerator CloseAnimation()
    {
        this.GetComponent<Animation>().Play("settingsmenuclose");
        yield return new WaitForSeconds(0.9f);
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
