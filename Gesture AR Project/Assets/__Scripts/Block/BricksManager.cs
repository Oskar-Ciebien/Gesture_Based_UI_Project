using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksManager : MonoBehaviour
{

    private static BricksManager _instance;
    public static BricksManager Instance => _instance;


    private int maxRows = 17;
    private int maxCols = 12;
    private GameObject bricksContainer;
    private GameObject objects;
    private float initalSpawnBrickPositionX = -4.72f;
    private float initalSpawnBrickPositionY = 13.2f;
    [SerializeField] float shiftAmount = 2f;

    public Material[] materials;
    public Color[] BrickColours;
    public Block brickPrefab;

    public List<Block> RemainingBricks { get; set; }

    public List<int[,]> LevelsData {get; set;}

    public int InitialBrickCount { get; set; }

    public int CurrentLevel;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        this.bricksContainer = new GameObject("BricksContainer");
        objects = GameObject.Find("GameObjects");
        
        this.bricksContainer.transform.parent = objects.transform;
        this.bricksContainer.transform.position = objects.transform.position;
        this.bricksContainer.transform.localScale = objects.transform.localScale;
        this.RemainingBricks = new List<Block>();
        this.LevelsData = this.LoadLevelsData();
        this.GenerateBricks();
    }

    private void GenerateBricks()
    {
        int[,] currentLevelData = this.LevelsData[this.CurrentLevel];
        float currentSpawnX = initalSpawnBrickPositionX;
        float currentSpawnY = initalSpawnBrickPositionY;
        float zShift = 2;

        for (int row = 0; row < this.maxRows; row++)
        {
            for (int col = 0; col < this.maxCols; col++)
            {
                int brickType = currentLevelData[row, col];

                if(brickType > 0)
                {
                   Block newBlock = Instantiate(brickPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f - zShift), Quaternion.identity) as Block;
                    newBlock.Init(bricksContainer.transform, this.materials[brickType - 1], this.BrickColours[brickType], brickType);

                    this.RemainingBricks.Add(newBlock);
                    zShift += 0.0001f;
                }

                currentSpawnX += shiftAmount;
                if(col + 1 == this.maxCols)
                {
                    currentSpawnX = initalSpawnBrickPositionX;
                }
            }
            currentSpawnY -= shiftAmount;
        }

        this.InitialBrickCount = this.RemainingBricks.Count;
    }

    private List<int[,]> LoadLevelsData()
    {
        TextAsset text = Resources.Load("levels") as TextAsset;

        string[] rows = text.text.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);

        List<int[,]> levelsData = new List<int[,]>();
        int[,] currentLevel = new int[maxRows, maxCols];
        int currentRow = 0;

        for(int row = 0; row < rows.Length; row++)
        {
            string line = rows[row];

            if(line.IndexOf("--") == -1)
            {
                string[] bricks = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for(int col = 0; col < bricks.Length; col++)
                {
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
