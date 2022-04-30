using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public GameObject WallContact;
    public static float initialSpeed = 20f;

    private Vector3 paddlePos;
    private static Vector3 startingPos;

    private static GameObject ball;
    private static Rigidbody rb;

    private void Start()
    {
        ball = this.gameObject;

        Vector3 paddlePos = PaddleBehaviour.playerPos;
        Vector3 startingPos = new Vector3(paddlePos.x, paddlePos.y + 0.5f, 0);

        // Move ball to starting position
        ball.transform.position = startingPos;
        rb = ball.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Constant Same Speed of Ball
        rb.velocity = rb.velocity.normalized * initialSpeed;
    }

    public static void StartBall()
    {
        // If Game Started
        if (GameManager.gameStarted == false)
        {
            rb.isKinematic = false;
            rb.AddForce(new Vector2(0, initialSpeed));

            GameManager.gameStarted = true;

            print("Game Started!");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "LeftBorder" || other.gameObject.tag == "RightBorder" || other.gameObject.tag == "TopBorder")
        {
            // Bounce
            print("Ball Bounced, Border: " + other.gameObject.tag);
            foreach (ContactPoint contact in other.contacts)
            {
                //Instantiate your particle system here.
                GameObject part = Instantiate(WallContact, contact.point, Quaternion.identity);
                part.GetComponent<ParticleSystem>().Play();
                Destroy(part, 3);

            }
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
            print("Hit something else!!!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BottomBorder")
        {
            // Player Dead
            print("Player Dead! Bottom Border hit!");
            PaddleBehaviour.Dead();
        }
    }
}
