using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePlayer : Collectible
{
    protected override void AddEffect()
    {
        // Set PaddleBehaviour
        var behaviour = PaddleBehaviour.player.GetComponent<PaddleBehaviour>();

        // Freeze Player
        behaviour.FrozenPlayer();
    }
}
