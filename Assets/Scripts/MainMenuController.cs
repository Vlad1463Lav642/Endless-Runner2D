using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}