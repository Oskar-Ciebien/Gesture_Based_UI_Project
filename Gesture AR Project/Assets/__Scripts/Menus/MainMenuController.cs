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
            // Set Starting Lives
            PlayerPrefs.SetInt("Lives", GameManager.startingLives);

            // Change to Game Scene
            SceneManager.LoadScene("Game Scene");
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
