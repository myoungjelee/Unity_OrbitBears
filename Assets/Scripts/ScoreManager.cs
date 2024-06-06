using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public static ScoreManager instance;

    public int score;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance );
        }
        score = 0;
        UpdateScoreText();
    }

    public void AddScore(int addScore)
    {
        this.score += score;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}

