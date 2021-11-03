using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    public static int jumpsLeft = 5;

    [SerializeField] float timeRemaining = 50;
    private bool timeRunning = false;

    public TextMeshProUGUI jumpsText;
    public TextMeshProUGUI timerText;


    void Start()
    {
        jumpsText = GetComponent<TextMeshProUGUI>();

        timeRunning = true;
        

    }

    void Update()
    {
        jumpsText.text = "Jumps Left: " + jumpsLeft;
        timerText.text = "Time: " + timeRemaining;

        TimerFunction();
    }

    private void TimerFunction()
    {
        if (timeRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Scene scene = SceneManager.GetActiveScene();
                timeRemaining = 0;
                timeRunning = false;

            }
        }
    }

    private void DisplayTime(float timeToDisplay)
    {

    }




}
