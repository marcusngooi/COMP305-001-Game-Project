using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public void LevelMenu()
    {
        this.gameObject.SetActive(true);
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
