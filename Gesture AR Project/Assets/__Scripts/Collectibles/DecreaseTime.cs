using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseTime : Collectible
{
    private int decreaseAmount = 5;
    protected override void AddEffect()
    {
        GameData.DecreaseScore(decreaseAmount);
    }
}