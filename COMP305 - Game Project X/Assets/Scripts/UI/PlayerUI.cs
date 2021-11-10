using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] public float time = 0;
    private bool timeRunning = true;

    public TextMeshProUGUI timerText;

    void Start()
    {
        time = 0;
    }

    void FixedUpdate()
    {
        timerText.text = "Time: " + time;

        TimerFunction();
    }

    private void TimerFunction()
    {
        if (timeRunning)
        {
                time += Time.fixedDeltaTime;
                time = Mathf.Round(time *100) / 100;
        }
    }

    private void DisplayTime(float timeToDisplay)
    {

    }




}
