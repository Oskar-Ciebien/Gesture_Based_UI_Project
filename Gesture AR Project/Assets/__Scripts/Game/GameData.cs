using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    // == Public Fields ==
    public static GameData singleton;
    public TextMeshProUGUI scoreText = null;
    public TextMeshProUGUI livesText = null;

    // == Private Fields ==
    private static int score = 0;
    private float lastUpdate = 0;
    private int oneSecond = 1;

    private void Awake()
    {
        // Create singleton
        GameObject[] go = GameObject.FindGameObjectsWithTag("GameData");

        // If there is more than one GameData
        if (go.Length > 1)
        {
            // Destroy it
            Destroy(this.gameObject);
        }
        else // Otherwise
        {
            // Don't destroy it
            DontDestroyOnLoad(this.gameObject);
            // Set it as Singleton
            singleton = this;
        }

        if (SceneManager.GetActiveScene().name == "Game Scene")
        {
            score = 0;
            // Set score to player prefs
            PlayerPrefs.SetInt("Score", score);
        }

        // Set to Texts
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
        livesText.text = PlayerPrefs.GetInt("Lives").ToString();
    }

    void Update()
    {
        // If game started
        if (GameManager.instance.gameStarted == true)
        {
            // If time is just one second from last update
            if (Time.time - lastUpdate >= 1f)
            {
                // Add to score
                score += oneSecond;

                // Set the new last update
                lastUpdate = Time.time;
            }
        }

        // print("Score: " + score);

        // Set score to player prefs
        PlayerPrefs.SetInt("Score", score);

        // If score on screen is not set
        if (scoreText != null)
        {
            // Set it
            scoreText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score").ToString();
        }

        // If livesText is not null
        if (livesText != null)
        {
            // Set the lives to the text
            livesText.text = PlayerPrefs.GetInt("Lives").ToString();
        }
    }

    public static void DecreaseScore(int decreaseAmount)
    {
        // If score is greater than the decrease amount
        if (score > decreaseAmount)
        {
            // Decrease the score
            score -= decreaseAmount;

            // Set the new score to player prefs
            PlayerPrefs.SetInt("Score", score);
        }
    }
}