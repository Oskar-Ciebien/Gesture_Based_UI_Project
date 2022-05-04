using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // == Serialized Fields ==
    [SerializeField] private float collectibleChance;
    [SerializeField] private GameObject[] possibleCollectibles;

    // == Private Fields ==
    private GameObject block;
    private MeshRenderer mat;
    private int Hitpoints = 1;

    private void Awake()
    {
        this.mat = this.GetComponent<MeshRenderer>();
    }

    void Start()
    {
        // Instantiate block object
        block = this.gameObject;
    }

    private void DestroyWithCollectible()
    {
        // Destroy the block
        Destroy(gameObject);

        // If Array is 0, return
        if (possibleCollectibles.Length == 0)
        {
            return;
        }

        if (UnityEngine.Random.Range(0, 1) < collectibleChance)
        {
            // Set random collectible from array
            var randomCollectible = possibleCollectibles[UnityEngine.Random.Range(0, possibleCollectibles.Length)];

            // Create it on same position and rotation as destroyed block
            Instantiate(randomCollectible, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // If collided with ball
        if (other.gameObject.tag == "Ball")
        {
            this.Hitpoints--;

            if (this.Hitpoints <= 0)
            {
                // Destroy the block and spawn collectible
                DestroyWithCollectible();
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
