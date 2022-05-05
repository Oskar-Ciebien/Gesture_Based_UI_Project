using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePlayer : Collectible
{
    // == Private Fields ==
    private static GameObject player = PaddleBehaviour.player;
    private static Vector2 playerPos;

    protected override void AddEffect()
    {
        var behaviour = PaddleBehaviour.player.GetComponent<PaddleBehaviour>();

        behaviour.FrozenPlayer();
    }
}
