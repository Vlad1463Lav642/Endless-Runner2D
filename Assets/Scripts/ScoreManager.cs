using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;

    [SerializeField] private float scoreCount;
    [SerializeField] private float highScoreCount;

    [SerializeField] private float pointsPerSecond;

    [SerializeField] private bool scoreIncreasing;

    private void Start()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }
    }

    private void Update()
    {
        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }

        if(scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount);
        }

        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        highScoreText.text = "High Score: " + Mathf.Round(highScoreCount);
    }

    public void AddScore(int pointsToAdd)
    {
        scoreCount += pointsToAdd;
    }

    public void SetScoreIncreasing(bool score)
    {
        scoreIncreasing = score;
    }

    public void SetScoreCount(float count)
    {
        scoreCount = count;
    }

    public float GetScoreCount()
    {
        return scoreCount;
    }
}