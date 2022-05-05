using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreCount : Collectible
{
    protected override void AddEffect()
    {
        // If not yet collected this collectible
        if (GameData.doubleIncrease == false)
        {
            // Add Effect
            GameData.doubleIncrease = true;
        }
    }
}
