using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // == Serialized Fields ==
    [SerializeField] private float collectibleChance;
    [SerializeField] private GameObject[] possibleCollectibles;

    // == Public Fields ==
    public GameObject breakEffect;
    public GameObject bounceEffect;
    public static event Action<Block> onBrickDestruction;

    // == Private Fields ==
    private GameObject block;
    private MeshRenderer mat;
    private int Hitpoints = 1;

    void Start()
    {
        // Instantiate block object
        block = this.gameObject;
    }

    private void Awake()
    {
        this.mat = this.GetComponent<MeshRenderer>();
    }

    // Destoy and spawn Collectible
    private void DestroyWithCollectible()
    {
        // Destroy the block
        Destroy(gameObject);

        // If Array is 0, return
        if (possibleCollectibles.Length == 0)
        {
            return;
        }

        // Random spawn chance
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
            // Bounce off the border
            // print("Ball Bounced - Border: " + other.gameObject.tag);

            foreach (ContactPoint contact in other.contacts)
            {
                //Instantiate your particle system here.
                GameObject partbounce = Instantiate(bounceEffect, contact.point, Quaternion.identity);
                SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Bounce);
                partbounce.GetComponent<ParticleSystem>().Play();
                Destroy(partbounce, 3);
                if (this.Hitpoints <= 0)
                {
                    GameObject partbreak = Instantiate(breakEffect, contact.point, Quaternion.identity);
                    SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Break);
                    partbreak.GetComponent<ParticleSystem>().Play();
                    Destroy(partbreak, 3);
                    onBrickDestruction?.Invoke(this);
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
    }

    public void Init(Transform containerTransform, Material material, Color color, int hitpoints)
    {
        this.transform.SetParent(containerTransform);
        this.mat.material = material;
        this.mat.material.color = color;
        this.Hitpoints = hitpoints;
    }
}
