using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    public void SettingsMenu()
    {
        this.gameObject.SetActive(true);

    }

    private IEnumerator StartAnimation()
    {
        this.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1f);
    }
}
