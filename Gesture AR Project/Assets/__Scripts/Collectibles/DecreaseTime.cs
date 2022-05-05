using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseTime : Collectible
{
    // == Private Fields ==
    private int decreaseAmount = 5;

    protected override void AddEffect()
    {
        // Decrease game score
        GameData.DecreaseScore(decreaseAmount);

        print("Decrease Time Collected!");
    }
}