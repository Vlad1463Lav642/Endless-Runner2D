using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Обеспечивает управление кнопками в главном меню игры.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string playGameLevel;
    [SerializeField] private string settings;

    [SerializeField] private AudioSource buttonPressedSound;

    /// <summary>
    /// Начать игру.
    /// </summary>
    public void PlayGame()
    {
        buttonPressedSound.Play();

        SceneManager.LoadScene(playGameLevel);
    }

    /// <summary>
    /// Перейти в настройки.
    /// </summary>
    public void SettingsGame()
    {
        buttonPressedSound.Play();

        SceneManager.LoadScene(settings);
    }

    /// <summary>
    /// Выйти из игры.
    /// </summary>
    public void QuitGame()
    {
        buttonPressedSound.Play();

        Application.Quit();
    }

    /// <summary>
    /// Открывает определенное окно.
    /// </summary>
    /// <param name="window">Объект окна.</param>
    public void OpenWindow(GameObject window)
    {
        window.SetActive(true);
    }

    /// <summary>
    /// Закрывает определенное окно.
    /// </summary>
    /// <param name="window">Объект окна.</param>
    public void CloseWindow(GameObject window)
    {
        window.SetActive(false);
    }

    /// <summary>
    /// Вход в существую учетную запись или регистрация новой.
    /// </summary>
    /// <param name="nickname">Имя игрока.</param>
    public void Authorization(InputField nickname)
    {
        if (PlayerPrefs.HasKey(nickname.text))
        {
            PlayerPrefs.SetString("CurrentPlayer", nickname.text);
        }
        else
        {
            PlayerPrefs.SetFloat(nickname.text, 0f);
            PlayerPrefs.SetString("CurrentPlayer", nickname.text);
        }
    }
}