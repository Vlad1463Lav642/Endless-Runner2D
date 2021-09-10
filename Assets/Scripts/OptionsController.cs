using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour
{
    private bool isReset;

    [SerializeField] private string mainMenuScene;
    [SerializeField] private AudioSource buttonPressedSound;


    public void ResetButton()
    {
        buttonPressedSound.Play();

        if (PlayerPrefs.HasKey("HighScore"))
        {
            isReset = true;
        }
    }

    public void SaveButton()
    {
        buttonPressedSound.Play();

        if (isReset)
        {
            PlayerPrefs.DeleteKey("HighScore");
        }

        SceneManager.LoadScene(mainMenuScene);
    }
}