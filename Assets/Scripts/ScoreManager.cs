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

    private string currentPlayer;

    [SerializeField] private bool scoreIncreasing;

    private void Start()
    {
        currentPlayer = PlayerPrefs.GetString("CurrentPlayer");

        if(PlayerPrefs.HasKey(currentPlayer) && currentPlayer != null)
        {
            highScoreCount = PlayerPrefs.GetFloat(currentPlayer);
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
            Debug.Log(currentPlayer);
            PlayerPrefs.SetFloat(currentPlayer, highScoreCount);
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