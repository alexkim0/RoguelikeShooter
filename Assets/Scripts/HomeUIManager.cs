using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HomeUIManager : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void StartGame()
    {
        // Start the game
        SceneManager.LoadScene("Desert Map");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToHome()
    {
        SceneManager.LoadScene("Home");
    }
}