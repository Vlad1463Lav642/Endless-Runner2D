using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private string mainMenuLevel;

    public virtual void RestartGame()
    {
        Time.timeScale = 1f;
        FindObjectOfType<GameManager>().ResetGame();
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuLevel);
    }
}