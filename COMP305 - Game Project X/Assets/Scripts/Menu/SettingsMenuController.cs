using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public EventSystem eventSystem;
    public Toggle fullscreenTog, vsyncTog;
    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;
    public TMP_Text resolutionLabel;

    public void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;
        if(QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }else
        {
            vsyncTog.isOn = true;
        }
        bool foundRes = false;

        for(int i = 0; i < resolutions.Count; i++)
        {
            if(Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedResolution = i;
                UpdateResLabel();
            }
        }
    }
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

    public void ApplyGraphics()
    {
        //Screen.fullScreen = fullscreenTog.isOn;

        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        } else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenTog.isOn);
    }
    public void ResLeft()
    {
        selectedResolution--;
        if(selectedResolution < 0)
        {
            selectedResolution = 0;
        }
        UpdateResLabel();
    }
    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = resolutions.Count - 1;
        }
        UpdateResLabel();
    }
    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();
    }
}
[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
