using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    private void Start()
    {
        BGMusic.BGInstance._audio.pitch = 1.0f;
        Time.timeScale = 1;
    }
    public void Play()
    {
        // If on main menu
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            // Change to level 1
            SceneManager.LoadScene("Level 1");
        }
    }

    public void Exit()
    {
        // If on main menu
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            // print("Quit the game from Main Menu!"); // Used for testing

            // Quits the game
            Application.Quit();
        }
    }
}
