using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject UI;
    [SerializeField] public int enemyKC, score;
    float timeMultiplier = 5f, time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void calculateScore()
    {
        time = UI.GetComponent<PlayerUI>().time;
        float timeD = time / 8;

        float scoreMultiplier = timeMultiplier - (timeMultiplier * timeD / (timeMultiplier + timeD));

        score = (int)((500 + (enemyKC * 50)) * scoreMultiplier);
        Debug.Log(score);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetFloat("time", time);
        PlayerPrefs.SetInt("enemyKC", enemyKC);
    }
}
