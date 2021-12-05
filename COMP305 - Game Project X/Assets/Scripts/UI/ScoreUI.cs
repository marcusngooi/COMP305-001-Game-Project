using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI killedText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lvlCompleteText;
    // Start is called before the first frame update
    void Start()
    {
        lvlCompleteText.text = "LEVEL " + (PlayerPrefs.GetInt("scene") - 1) + " CLEARED";
        killedText.text = PlayerPrefs.GetInt("enemyKC").ToString();
        timeText.text = PlayerPrefs.GetFloat("time").ToString("0.00");
        scoreText.text = PlayerPrefs.GetInt("score").ToString();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
