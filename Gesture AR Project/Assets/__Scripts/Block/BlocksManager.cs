using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlocksManager : MonoBehaviour
{
    // == Singleton Instance == 
    private static BlocksManager _instance;
    public static BlocksManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // == private variables == 
    private int maxRows = 17;
    private int maxCols = 12;
    private GameObject blocksContainer;
    private GameObject objects;
    private float initalSpawnBlockPositionX = -4.72f;
    private float initalSpawnBlockPositionY = 13.2f;

    // ==[SerializeField Fields]==
    [SerializeField] float shiftAmount = 2f;

    // == public  variables == 
    [Header("Block Settings")]
    public Block brickPrefab;
    public Material[] materials;
    public Color[] BrickColours;

    public List<Block> RemainingBricks { get; set; }
    public List<int[,]> LevelsData { get; set; }
    public int InitialBrickCount { get; set; }

    [Header("Set Current Level")]
    public int CurrentLevel;

    private void Start()
    {
        // Create a new object to hold all the new instantiated blocks
        this.blocksContainer = new GameObject("BlocksContainer");
        objects = GameObject.Find("GameObjects");

        // Set the BlockContainer to have the same transform position and scale as the GameObjects parent
        this.blocksContainer.transform.parent = objects.transform;
        this.blocksContainer.transform.position = objects.transform.position;
        this.blocksContainer.transform.localScale = objects.transform.localScale;

        // Level initialization
        this.CurrentLevel = PlayerPrefs.GetInt("Level");
        this.RemainingBricks = new List<Block>();
        this.LevelsData = this.LoadLevelsData();
        this.GenerateBricks();
    }

    // Method for selecting a random level of 4
    public void NewLevel()
    {
        this.CurrentLevel = Random.Range(0, 3);
        PlayerPrefs.SetInt("Level", this.CurrentLevel);
        PaddleBehaviour.RestartScene();
    }
    // Method for generating the loaded in configuration of blocks.
    private void GenerateBricks()
    {
        // Read in the Levels array
        int[,] currentLevelData = this.LevelsData[this.CurrentLevel];
        float currentSpawnX = initalSpawnBlockPositionX;
        float currentSpawnY = initalSpawnBlockPositionY;
        
        float zShift = 2;

        // Loop through the level data and instantiate a new block... 
        // ...depending on their position...
        for (int row = 0; row < this.maxRows; row++)
        {
            for (int col = 0; col < this.maxCols; col++)
            {
                // ...and depending on their type
                int brickType = currentLevelData[row, col];

                if (brickType > 0)
                {
                    Block newBlock = Instantiate(brickPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f - zShift), Quaternion.identity) as Block;
                    newBlock.Init(blocksContainer.transform, this.materials[brickType - 1], this.BrickColours[brickType], brickType);

                    this.RemainingBricks.Add(newBlock);
                    // Slightly adjust z axis so blocks don't overlap 
                    zShift += 0.0001f;
                }
                // Space out the blocks by an inputted amount (1.1)
                currentSpawnX += shiftAmount;

                // Once we're out of columns
                if (col + 1 == this.maxCols)
                {
                    // move on to the next column
                    currentSpawnX = initalSpawnBlockPositionX;
                }
            }
            currentSpawnY -= shiftAmount;
        }

        this.InitialBrickCount = this.RemainingBricks.Count;
    }

    // Method for reading in the level data, solution was adapted from https://www.youtube.com/watch?v=b_1DUKCInIw&t=1164s
    private List<int[,]> LoadLevelsData()
    {
        TextAsset text = Resources.Load("levels") as TextAsset;
        // Youtube comment to the rescue, adapted from "?????????? ??????" in the comment section of the above linked video
        string[] rows = text.text.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);

        List<int[,]> levelsData = new List<int[,]>();
        int[,] currentLevel = new int[maxRows, maxCols];
        int currentRow = 0;

        // Iterate through matrix 
        for (int row = 0; row < rows.Length; row++)
        {
            string line = rows[row];

            // When a "--" is met move on to the next matrix
            if (line.IndexOf("--") == -1)
            {
                string[] bricks = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < bricks.Length; col++)
                {
                    // Adapted from "Apparent Design" in the same comments section
                    int.TryParse(bricks[col], out currentLevel[currentRow, col]);
                }

                currentRow++;
            }
            else
            {
                currentRow = 0;
                levelsData.Add(currentLevel);
                currentLevel = new int[maxRows, maxCols];
            }
        }

        return levelsData;
    }

}
