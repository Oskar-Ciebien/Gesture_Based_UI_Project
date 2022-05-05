using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePlayer : Collectible
{
    // == Private Fields ==
    private int lives;

    protected override void AddEffect()
    {
        StartCoroutine(PreventMovement());
    }

    static IEnumerator PreventMovement()
    {
        float timePassed = 0;

        PaddleBehaviour.rb.constraints = RigidbodyConstraints.FreezeAll;

        while (timePassed < 3)
        {
            timePassed += Time.deltaTime;

            yield return null;
        }

        PaddleBehaviour.rb.constraints = RigidbodyConstraints.None;
    }
}
