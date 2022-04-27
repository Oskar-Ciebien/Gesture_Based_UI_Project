using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameStarted = false;
    public static int startingLives = 3;
    public static int lives;
    public static int startingScore = 0;
    public static int score;

   
    void Start()
    {
        gameStarted = false;
    }
}
