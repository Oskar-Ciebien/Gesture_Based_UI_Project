using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameData : MonoBehaviour
{
    // == Serialized Fields ==
    [SerializeField] TextMeshProUGUI scoreText;

    // == Public Fields ==
    public static GameData singleton;

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

        // Set the score on the screen
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }

    void Update()
    {
        // If game started
        if (GameManager.gameStarted == true)
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

        print("Score: " + score);

        // Set score to player prefs
        PlayerPrefs.SetInt("Score", score);

        // If score on screen is not set
        if (scoreText != null)
        {
            // Set it
            scoreText.text = PlayerPrefs.GetInt("Score").ToString();
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