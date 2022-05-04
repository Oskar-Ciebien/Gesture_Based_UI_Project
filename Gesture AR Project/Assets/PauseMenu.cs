using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        BGMusic.BGInstance._audio.pitch = 1.0f;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void Quit()
    {
        // Change scene to Main Menu Scene
        SceneManager.LoadScene("Main Menu");
    }
}
