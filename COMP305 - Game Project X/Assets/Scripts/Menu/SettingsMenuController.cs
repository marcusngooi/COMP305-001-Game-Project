using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public void SettingsMenu()
    {
        this.gameObject.SetActive(true);
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
