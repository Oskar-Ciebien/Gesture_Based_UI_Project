using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // == Private Fields ==
    private GameObject block;

    void Start()
    {
        // Instantiate block object
        block = this.gameObject;
    }

    private void OnCollisionEnter(Collision other)
    {
        // If collided with ball
        if (other.gameObject.tag == "Ball")
        {
            print("Block Destroyed");

            // Destroy the block
            Destroy(block);
        }
    }
}
