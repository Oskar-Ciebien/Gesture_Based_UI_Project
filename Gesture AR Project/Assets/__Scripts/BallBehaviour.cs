using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private GameObject ball;
    private Rigidbody rb;
    Vector3 paddlePos;
    Vector3 startingPos;

    public static float initialSpeed = 20f;

    private void Start()
    {
        ball = this.gameObject;

        Vector3 paddlePos = PlayerBehaviour.playerPos;
        Vector3 startingPos = new Vector3(paddlePos.x, paddlePos.y + 0.5f, 0);

        ball.transform.position = startingPos;
        rb = ball.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Constant Same Speed of Ball
        rb.velocity = rb.velocity.normalized * initialSpeed;

        // If Game Started
        if (GameManager.gameStarted == false)
        {
            // Move ball to starting position
            ball.transform.position = startingPos;

            // If player pressed Space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.isKinematic = false;
                rb.AddForce(new Vector2(0, initialSpeed));

                GameManager.gameStarted = true;

                print("Game Started!");
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "LeftBorder" || other.gameObject.tag == "RightBorder" || other.gameObject.tag == "TopBorder")
        {
            // Bounce
            print("Ball Bounced, Border: " + other.gameObject.tag);
        }
        else if (other.gameObject.tag == "Paddle")
        {
            // Hit Player
            print("Ball Bounced, Player!");
        }
        else if (other.gameObject.tag == "Block")
        {
            // Block Destroyed
            print("Block Hit!");
        }
        else
        {
            // Player Dead
            print("Player Dead! Bottom Border hit!");
            PlayerBehaviour.Dead();
        }
    }
}
