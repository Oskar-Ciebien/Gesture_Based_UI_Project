using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreCount : Collectible
{
    // == Private Fields ==
    private int doubleScore = 2;

    protected override void AddEffect()
    {
        if (GameData.doubleIncrease == false)
        {
            GameData.doubleIncrease = true;
        }
    }
}
