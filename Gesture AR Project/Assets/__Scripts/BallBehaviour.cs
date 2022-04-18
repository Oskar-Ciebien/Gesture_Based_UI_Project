using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 20f;

    GameObject ball;
    Rigidbody rb;

    private bool isBouncing = false;

    private Vector2 ballInitialForce;

    private Vector3 ballPosition;
    private Vector3 force;

    void Start()
    {
        ball = this.gameObject;

        ballInitialForce = new Vector2(50.0f, 80.0f);
        force = new Vector3(20f, 0f, 0f);

        isBouncing = false;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = rb.velocity.normalized * speed;

        // On Space
        if (Input.GetButtonDown("Jump") == true)
        {
            // Check if ball is bouncing
            if (!isBouncing)
            {
                // Add the initial Force
                rb.AddForce(ballInitialForce);

                isBouncing = !isBouncing;
            }
        }

        if (!isBouncing)
        {
            ballPosition.x = PlayerBehaviour.player.transform.position.x;

            transform.position = ballPosition;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "LeftBorder" || other.gameObject.tag == "RightBorder" || other.gameObject.tag == "TopBorder")
        {
            // Bounce
            print("Ball Bounced, Border: " + other.gameObject.tag);

            // rb.AddForce(force * speed);
        }
        else if (other.gameObject.tag == "Player")
        {
            // Hit Player
            print("Ball Bounced, Player!");

            // rb.AddForce(force * speed);
        }
        else
        {
            // Player Dead
            PlayerBehaviour.Die();
        }
    }
}
