using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    public static GameData singleton;
    private static int score = 0;
    private float lastUpdate = 0;

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

        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }

    void Update()
    {
        if (GameManager.gameStarted == true)
        {
            if (Time.time - lastUpdate >= 1f)
            {
                score += 1;
                lastUpdate = Time.time;
            }
        }
        print("Score: " + score);

        PlayerPrefs.SetInt("Score", score);

        if (scoreText != null)
        {
            scoreText.text = PlayerPrefs.GetInt("Score").ToString();
        }
    }

    public static void DecreaseScore(int decreaseAmount)
    {
        if (score > decreaseAmount)
        {
            score -= decreaseAmount;

            PlayerPrefs.SetInt("Score", score);
        }
    }
}