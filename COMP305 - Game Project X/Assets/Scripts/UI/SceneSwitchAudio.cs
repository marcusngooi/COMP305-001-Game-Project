using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchAudio : MonoBehaviour
{
    private static SceneSwitchAudio instance = null;
    public static SceneSwitchAudio Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
