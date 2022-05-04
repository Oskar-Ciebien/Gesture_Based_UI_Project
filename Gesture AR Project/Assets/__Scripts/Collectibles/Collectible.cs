using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    // == Private Fields ==
    private float speed = 3f;

    void Update()
    {
        // Move down
        transform.Translate(Vector3.back * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // On collision with Paddle or Bottom Border
        if (other.tag == "Paddle" || other.tag == "BottomBorder")
        {
            // Destroy the collectible
            Destroy(this.gameObject);
        }

        // If collided with Paddle
        if (other.tag == "Paddle")
        {
            // Add an effect to the game
            this.AddEffect();
        }
    }

    protected abstract void AddEffect();
}
