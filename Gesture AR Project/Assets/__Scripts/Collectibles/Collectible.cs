using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Paddle" || other.tag == "BottomBorder")
        {
            Destroy(this.gameObject);
        }

        if (other.tag == "Paddle")
        {
            this.AddEffect();
        }
    }

    protected abstract void AddEffect();
}
