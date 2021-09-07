using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;

    private void Awake()
    {
        Cursor.SetCursor(cursorTexture,Vector2.zero,CursorMode.ForceSoftware);
    }

    [SerializeField] private string playGameLevel;

    public void PlayGame()
    {
        SceneManager.LoadScene(playGameLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}