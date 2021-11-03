using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    public static int jumpsLeft = 5;

    [SerializeField] float time = 0;
    private bool timeRunning = false;

    public TextMeshProUGUI jumpsText;
    public TextMeshProUGUI timerText;


    void Start()
    {
        jumpsText = GetComponent<TextMeshProUGUI>();
        time = 0;
        timeRunning = true;
        

    }

    void Update()
    {
        jumpsText.text = "Jumps Left: " + jumpsLeft;
        timerText.text = "Time: " + time;

        TimerFunction();
    }

    private void TimerFunction()
    {
        if (timeRunning)
        {
                time += Time.deltaTime;
                time = Mathf.Round(time *100) / 100;
        }
    }

    private void DisplayTime(float timeToDisplay)
    {

    }




}
