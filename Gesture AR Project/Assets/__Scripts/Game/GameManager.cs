using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : CustomDefaultTrackableEventHandler
{
    // == Serialized Fields ==
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameObject gameObjects;
    [SerializeField] private GameObject startPanel;

    // == Public Fields ==
    public static GameManager instance;
    public static bool gameStarted = false;
    public static int startingLives = 3;
    public static int lives;
    public static int startingScore = 0;
    public static int score;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        if (CustomDefaultTrackableEventHandler.TrueFalse == true)
        {
            // Enable UI and Game Objects
            uiPanel.SetActive(true);
            gameObjects.SetActive(true);

            // Disable start info
            startPanel.SetActive(false);

            print("Game and UI Set! - Game Manager");
        }
    }
}
