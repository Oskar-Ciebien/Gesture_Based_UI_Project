using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void Play()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            SceneManager.LoadScene("Level 1");
        }
    }

    public void Exit()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            // print("Quit the game from Main Menu!"); // Used for testing

            // Quits the game
            Application.Quit();
        }
    }
}
