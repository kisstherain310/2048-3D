using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text  scoreUI;
    [SerializeField] private Text  highScoreUI;
    private int score = 0;
    private int highScore = -1;
    public void InitScore()
    {
        scoreUI.text = "0";
        highScoreUI.text = "Tốt nhất: 2048";
    }
    public void ResetScore()
    {
        score = 0;
        scoreUI.text = "0";
    }
    public void AddScore(int addScore)
    {
        score += addScore;
        scoreUI.text = score.ToString();
    }
    public void SetHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            highScoreUI.text = "Tốt nhất: " + highScore.ToString();
        }
    }
}
