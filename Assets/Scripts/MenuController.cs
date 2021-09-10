using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private string mainMenuLevel;

    [SerializeField] private AudioSource buttonPressedSound;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        buttonPressedSound.Play();

        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        buttonPressedSound.Play();

        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

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

    public void ExitToMainMenu()
    {
        buttonPressedSound.Play();

        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuLevel);
    }
}