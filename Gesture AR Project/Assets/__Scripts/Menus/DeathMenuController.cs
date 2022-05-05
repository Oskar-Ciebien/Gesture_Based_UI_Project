using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuController : MonoBehaviour
{
    // == Private Fields ==
    public static bool isRestarted;

    private void Start()
    {
        BGMusic.BGInstance._audio.pitch = 0.65f;
    }

    void Awake()
    {
        isRestarted = false;
    }

    public void MainMenu()
    {
        // If on the Death Scene
        if (SceneManager.GetActiveScene().name == "Death Scene")
        {
            isRestarted = true;

            // Change scene to Main Menu Scene
            SceneManager.LoadScene("Main Menu");
        }
    }

    public void Restart()
    {
        // If on the Death Scene
        if (SceneManager.GetActiveScene().name == "Death Scene")
        {
            // Game restarted
            isRestarted = true;

            // Change scene to Game Scene
            SceneManager.LoadScene("Game Scene", LoadSceneMode.Single);
        }
    }
}
