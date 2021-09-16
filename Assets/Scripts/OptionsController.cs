using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Обеспечивает управление кнопками в настройках игры.
/// </summary>
public class OptionsController : MonoBehaviour
{
    private bool isReset;

    [SerializeField] private string mainMenuScene;
    [SerializeField] private AudioSource buttonPressedSound;

    private string currentPlayer;

    private void Start()
    {
        currentPlayer = PlayerPrefs.GetString("CurrentPlayer");
    }

    /// <summary>
    /// Сброс рекорда.
    /// </summary>
    public void ResetButton()
    {
        buttonPressedSound.Play();

        if (PlayerPrefs.HasKey(currentPlayer) && currentPlayer != null)
        {
            isReset = true;
        }
    }
    
    /// <summary>
    /// Сохранение изменений.
    /// </summary>
    public void SaveButton()
    {
        buttonPressedSound.Play();

        if (isReset)
        {
            PlayerPrefs.SetFloat(currentPlayer, 0f);
        }

        SceneManager.LoadScene(mainMenuScene);
    }
}