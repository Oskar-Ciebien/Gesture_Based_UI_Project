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
            // Reset Player
            PaddleBehaviour.ResetPlayer();

            // Change scene to Game Scene
            SceneManager.LoadScene("Game Scene", LoadSceneMode.Single);
        }
    }
}
