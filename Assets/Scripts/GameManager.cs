using UnityEngine;

/// <summary>
/// Обеспечивает управление состоянием игры.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform platformGenerator;
    private Vector3 platformStartPoint;

    private PlayerController player;
    private Vector3 playerStartPoint;

    private PlatformDestroyer[] PlatformDestroyersList;

    private ScoreManager scoreManager;

    [SerializeField] private GameObject gameOver;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        platformStartPoint = platformGenerator.position;
        playerStartPoint = player.transform.position;

        scoreManager = FindObjectOfType<ScoreManager>();
        gameOver.gameObject.SetActive(false);
    }

    public void Restart()
    {
        scoreManager.SetScoreIncreasing(false);
        player.gameObject.SetActive(false);

        gameOver.gameObject.SetActive(true);
    }

    /// <summary>
    /// Сброс игры.
    /// </summary>
    public void ResetGame()
    {
        gameOver.gameObject.SetActive(false);

        PlatformDestroyersList = FindObjectsOfType<PlatformDestroyer>();

        foreach (var destroyer in PlatformDestroyersList)
        {
            destroyer.gameObject.SetActive(false);
        }

        player.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        player.gameObject.SetActive(true);

        player.SetTransformSkeleton(false);

        scoreManager.SetScoreCount(0);
        scoreManager.SetScoreIncreasing(true);
    }
}