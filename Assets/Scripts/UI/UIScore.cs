using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    [SerializeField] TMP_Text textScore;
    [SerializeField] TMP_Text textHighScore;

    private void Start()
    {
        //int highScore = GetHighScore();
        //textHighScore.text = "HighScore: " + highScore;
    }

    public void UpdateScore(int score)
    {
        textScore.text = "Score: " + score;
        Debug.Log(score);
        //SetHighScore(score);
    }
/*
    int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }

    private void SetHighScore(int score)
    {
        if (score >= PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
            PlayerPrefs.Save();
            Debug.Log("New High Score");
        }
    }*/
}
