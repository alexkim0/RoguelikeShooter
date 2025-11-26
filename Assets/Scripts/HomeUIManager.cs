using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HomeUIManager : MonoBehaviour
{
    public void StartGame()
    {
        // Start the game
        SceneManager.LoadScene("Desert Map");
    }
}