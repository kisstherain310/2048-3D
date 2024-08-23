using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreUI;
    [SerializeField] private TMP_Text highScoreUI;
    public int score = 0;
    public int highScore = 0;
    void Awake()
    {
        score = 0;
        highScore = 0;
        scoreUI.text = "0";
        highScoreUI.text = "Tốt nhất: 0";
    }
    public void AddScore(int addScore)
    {
        score += addScore;
        scoreUI.text = score.ToString();
    }
    public void SetScore(int score, int highScore)
    {
        this.score = score;
        this.highScore = highScore;
        scoreUI.text = score.ToString();
        highScoreUI.text = "Tốt nhất: " + highScore.ToString();
    }
    public void EditScore(){
        if (score > highScore)
        {
            highScore = score;
            highScoreUI.text = "Tốt nhất: " + highScore.ToString();
        }
        score = 0;
        scoreUI.text = score.ToString();
    }
}
