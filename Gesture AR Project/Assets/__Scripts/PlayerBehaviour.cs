using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 20f;

    public static GameObject player;
    public static Vector3 playerPos;

    private Rigidbody rb;

    private float leftWallPos = -10f;
    private float rightWallPos = 10f;
    private float force = 10f;

    void Start()
    {
        player = this.gameObject;

        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (playerPos.x < leftWallPos)
        {
            print("Player collided with a wall");

            playerPos = player.transform.position;
            player.transform.Translate(new Vector3(0, 0.05f, 0));
        }
        else if (playerPos.x > rightWallPos)
        {
            print("Player collided with a wall");

            playerPos = player.transform.position;
            player.transform.Translate(new Vector3(0, -0.05f, 0));
        }
        else
        {
            playerPos = player.transform.position;
            player.transform.Translate(new Vector3(0, Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0));
        }
    }

    private static void ResetPlayer()
    {
        GameManager.lives = GameManager.startingLives;
    }

    public static void Dead()
    {
        if (GameManager.lives > 1 && GameManager.lives <= 3)
        {
            RestartScene();
        }
        else
        {
            DeathScene();
        }
    }

    // Restart the Game - Player Died (Still has lives)
    private static void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Load the Scene
        SceneManager.LoadScene(currentScene.name, LoadSceneMode.Single);
    }

    // Show the DeathScene - Player Died (No more lives)
    private static void DeathScene()
    {
        // Call ResetPlayer()
        ResetPlayer();
        // Load the Scene
        // SceneManager.LoadScene("DeathScene", LoadSceneMode.Single);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Rigidbody ballRB = other.gameObject.GetComponent<Rigidbody>();

            Vector3 hitPoint = other.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            ballRB.velocity = Vector2.zero;

            float difference = paddleCenter.x - hitPoint.x;

            // Left
            if (hitPoint.x < paddleCenter.x)
            {
                ballRB.AddForce(new Vector2(-(Mathf.Abs(difference * force)), BallBehaviour.initialSpeed));
            }
            else
            {
                ballRB.AddForce(new Vector2((Mathf.Abs(difference * force)), BallBehaviour.initialSpeed));
            }
        }
    }
}
