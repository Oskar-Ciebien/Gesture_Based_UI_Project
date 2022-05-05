using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreCount : Collectible
{
    protected override void AddEffect()
    {
        if (GameData.doubleIncrease == false)
        {
            GameData.doubleIncrease = true;
        }
    }
}
