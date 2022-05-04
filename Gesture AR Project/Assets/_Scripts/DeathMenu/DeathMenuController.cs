using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuController : MonoBehaviour
{

    private void Start()
    {
        BGMusic.BGInstance._audio.pitch = 0.65f;
    }


    public void MainMenu()
    {
        // If on the Death Scene
        if (SceneManager.GetActiveScene().name == "Death Scene")
        {
            // Change scene to Main Menu Scene
            SceneManager.LoadScene("Main Menu");
        }
    }

    public void Restart()
    {
        // If on the Death Scene
        if (SceneManager.GetActiveScene().name == "Death Scene")
        {
            // Change scene to Main Menu Scene
            SceneManager.LoadScene("Level 1");
        }
    }

    public void Exit()
    {
        // If on the Death Scene
        if (SceneManager.GetActiveScene().name == "Death Scene")
        {
            // print("Quit the game from Death Scene!"); // Used for testing

            // Quits the game
            Application.Quit();
        }
    }
}
