using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Обеспечивает управление счетом игрока.
/// </summary>
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

    /// <summary>
    /// Добавляет определенное количество очков к счету.
    /// </summary>
    /// <param name="pointsToAdd">Количество очков для добавления.</param>
    public void AddScore(int pointsToAdd)
    {
        scoreCount += pointsToAdd;
    }

    public void SetScoreIncreasing(bool score)
    {
        scoreIncreasing = score;
    }

    /// <summary>
    /// Устанавливает определенное количество очков у игрока.
    /// </summary>
    /// <param name="count">Количество очков.</param>
    public void SetScoreCount(float count)
    {
        scoreCount = count;
    }

    /// <summary>
    /// Возвращает количество очков игрока.
    /// </summary>
    /// <returns>Количество очков.</returns>
    public float GetScoreCount()
    {
        return scoreCount;
    }
}