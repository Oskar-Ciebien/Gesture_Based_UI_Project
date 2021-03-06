using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseTime : Collectible
{
    // == Private Fields ==
    private int increaseMinAmount = 20;
    private int increaseMaxAmount = 50;
    private int minAmount = 10;
    private int maxAmount = 30;

    protected override void AddEffect()
    {
        // If player already collected double score count collectible
        if (GameData.doubleIncrease == true)
        {
            // Decrease game score * 2 - Make it easier a little bit
            GameData.DecreaseScore(Random.Range(increaseMinAmount, increaseMaxAmount));
        }
        else
        {
            // Decrease game score
            GameData.DecreaseScore(Random.Range(minAmount, maxAmount));
        }

        // print("Decrease Time Collected!");
    }
}