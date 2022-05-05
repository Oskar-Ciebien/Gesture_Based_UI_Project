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
    public bool gameStarted { get; set; }
    public static int startingLives = 3;
    public static int startingScore = 0;

    private void Awake()
    {
        // Initialise the instance
        if (instance == null) instance = this;
        Block.onBrickDestruction += onBlockDestruction;
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

            // print("Game and UI Set! - Game Manager");
        }
    }

    private void onBlockDestruction(Block obj)
    {
        if (BricksManager.Instance.RemainingBricks.Count <= 0)
        {
            gameStarted = false;
            BricksManager.Instance.NewLevel();
        }
    }
}
