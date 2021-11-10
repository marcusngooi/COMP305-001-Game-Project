using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchAudio : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
