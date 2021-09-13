using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void ResetButton()
    {
        buttonPressedSound.Play();

        if (PlayerPrefs.HasKey(currentPlayer) && currentPlayer != null)
        {
            isReset = true;
        }
    }

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