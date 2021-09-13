using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string playGameLevel;
    [SerializeField] private string settings;

    [SerializeField] private AudioSource buttonPressedSound;

    public void PlayGame()
    {
        buttonPressedSound.Play();

        SceneManager.LoadScene(playGameLevel);
    }

    public void SettingsGame()
    {
        buttonPressedSound.Play();

        SceneManager.LoadScene(settings);
    }

    public void QuitGame()
    {
        buttonPressedSound.Play();

        Application.Quit();
    }

    public void OpenWindow(GameObject window)
    {
        window.SetActive(true);
    }

    public void CloseWindow(GameObject window)
    {
        window.SetActive(false);
    }

    public void Login(InputField nickname)
    {
        if (PlayerPrefs.HasKey(nickname.text))
        {
            PlayerPrefs.SetString("CurrentPlayer", nickname.text);
        }
    }

    public void Registration(InputField nickname)
    {
        if (!PlayerPrefs.HasKey(nickname.text))
        {
            PlayerPrefs.SetFloat(nickname.text, 0f);
            PlayerPrefs.SetString("CurrentPlayer", nickname.text);
        }
    }
}