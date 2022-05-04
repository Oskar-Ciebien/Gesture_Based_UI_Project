using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // == Private Fields ==
    private GameObject block;
    private MeshRenderer mat;
    public int Hitpoints = 1;

    private void Awake()
    {
        this.mat = this.GetComponent<MeshRenderer>();
    }
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
            this.Hitpoints--;
            print("Block Destroyed");

            if(this.Hitpoints <= 0)
            {
                // Destroy Effect
                // Destroy the block
                Destroy(block);
            }
            else
            {
                // Change material 
                this.mat.material = BricksManager.Instance.materials[this.Hitpoints - 1];
            }
           
        }
    }

    public void Init(Transform containerTransform, Material material, Color color, int hitpoints)
    {
        this.transform.SetParent(containerTransform);
        this.mat.material = material;
        this.mat.material.color = color;
        this.Hitpoints = hitpoints;
    }
}
