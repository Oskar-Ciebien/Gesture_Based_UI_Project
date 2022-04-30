using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    GameObject block;

    void Start()
    {
        block = this.gameObject;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            print("Block Destroyed");

            Destroy(block);
        }
    }
}
