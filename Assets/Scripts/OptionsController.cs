using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour
{
    private bool isReset;

    [SerializeField] private string mainMenuScene;


    public void ResetButton()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            isReset = true;
        }
    }

    public void SaveButton()
    {
        if (isReset)
        {
            PlayerPrefs.DeleteKey("HighScore");
        }

        SceneManager.LoadScene(mainMenuScene);
    }
}