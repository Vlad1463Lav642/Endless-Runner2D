using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Обеспечивает управление кнопками во время игры.
/// </summary>
public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private string mainMenuLevel;

    [SerializeField] private AudioSource buttonPressedSound;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    /// <summary>
    /// Остановка игры.
    /// </summary>
    public void PauseGame()
    {
        buttonPressedSound.Play();

        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    /// <summary>
    /// Возврат в игру.
    /// </summary>
    public void ResumeGame()
    {
        buttonPressedSound.Play();

        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    /// <summary>
    /// Перезапуск игры.
    /// </summary>
    public void RestartGame()
    {
        buttonPressedSound.Play();

        Time.timeScale = 1f;
        FindObjectOfType<GameManager>().ResetGame();

        if(pauseMenu.activeSelf == true)
        {
            pauseMenu.SetActive(false);
        }
    }

    /// <summary>
    /// Выход в главное меню.
    /// </summary>
    public void ExitToMainMenu()
    {
        buttonPressedSound.Play();

        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuLevel);
    }
}