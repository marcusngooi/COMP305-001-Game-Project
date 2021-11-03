using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI killedText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        killedText.text = PlayerPrefs.GetInt("enemyKC").ToString();
        timeText.text = (Mathf.Round(PlayerPrefs.GetFloat("time") * 100) / 100).ToString();
        scoreText.text = PlayerPrefs.GetInt("score").ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
