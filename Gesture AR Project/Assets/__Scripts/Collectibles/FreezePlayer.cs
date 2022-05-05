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
        // playerPos = player.transform.position;

        StartCoroutine(PreventMovement());
    }

    IEnumerator PreventMovement()
    {
        // PaddleBehaviour.rb.constraints = RigidbodyConstraints.FreezeAll;

        // PaddleBehaviour.frozen = true;

        PaddleBehaviour.FrozenPlayer(true);

        // player.transform.Translate(new Vector2(player.transform.position.x, player.transform.position.y));
        print("Frozen?: " + PaddleBehaviour.frozen);

        yield return new WaitForSeconds(1f);

        PaddleBehaviour.FrozenPlayer(false);

        print("Frozen Here: " + PaddleBehaviour.frozen);
        // PaddleBehaviour.rb.constraints = RigidbodyConstraints.None;
    }
}
