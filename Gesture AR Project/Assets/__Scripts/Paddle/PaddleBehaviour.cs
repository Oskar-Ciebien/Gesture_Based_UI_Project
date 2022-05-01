using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleBehaviour : MonoBehaviour
{
    // == Public Fields ==
    public static GameObject player;
    public static Vector3 playerPos;
    public GameObject BallContact;
    public GameObject WallContact;

    // == Private Fields ==
    private Rigidbody rb;
    private Material m_Material;
    private static float leftWallPos = -10f;
    private static float rightWallPos = 10f;
    private float force = 10f;

    void Start()
    {
        // Initialise the instance
        player = this.gameObject;

        // Set the Components
        rb = player.GetComponent<Rigidbody>();
        m_Material = GetComponent<Renderer>().material;
        m_Material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        playerPos = player.transform.position;
    }

    public static void Movement(Vector2 touchPosition)
    {
        // Touched left border
        if (playerPos.x < leftWallPos)
        {
            print("Player collided with left border!");

            // Move player back inside the borders
            playerPos = player.transform.position;
            player.transform.Translate(new Vector3(0, 0.1f, 0));
        }
        // Touched right border
        else if (playerPos.x > rightWallPos)
        {
            print("Player collided with right border!");

            // Move player back inside the borders
            playerPos = player.transform.position;
            player.transform.Translate(new Vector3(0, -0.1f, 0));
        }
        else
        {
            // Player Moving
            playerPos = player.transform.position;
            player.transform.Translate(new Vector3(0, touchPosition.x * Time.deltaTime, 0));
        }
    }

    private static void ResetPlayer()
    {
        // Reset lives
        GameManager.lives = GameManager.startingLives;
    }

    public static void Dead()
    {
        // If still enough lives left
        if (GameManager.lives > 1 && GameManager.lives <= 3)
        {
            // Restart Scene
            RestartScene();
        }
        // If no more lives left
        else
        {
            // Set the death scene
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
        SceneManager.LoadScene("Death Scene", LoadSceneMode.Single);
    }

    private void OnCollisionEnter(Collision other)
    {
        // If collided with the ball
        if (other.gameObject.tag == "Ball")
        {
            // Get rigid body of the ball
            Rigidbody ballRB = other.gameObject.GetComponent<Rigidbody>();

            // Set the hit point and center of the paddle
            Vector3 hitPoint = other.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            // Set the ball's velocity to zero
            ballRB.velocity = Vector2.zero;

            // Get the different from center and hit point
            float difference = paddleCenter.x - hitPoint.x;

            foreach (ContactPoint contact in other.contacts)
            {
                //Instantiate your particle system here.
                GameObject part = Instantiate(BallContact, contact.point, Quaternion.identity);
                part.GetComponent<ParticleSystem>().Play();
                StartCoroutine(Flash(m_Material));
                Destroy(part, 3);
            }

            // Left Side of paddle hit
            if (hitPoint.x < paddleCenter.x)
            {
                // Add force to the ball to the left
                ballRB.AddForce(new Vector2(-(Mathf.Abs(difference * force)), BallBehaviour.initialSpeed));
            }
            else // Right side of paddle hit
            {
                // Add force to the ball to the right
                ballRB.AddForce(new Vector2((Mathf.Abs(difference * force)), BallBehaviour.initialSpeed));
            }
        }

        // If hit the left, top and right border
        if (other.gameObject.tag == "LeftBorder" || other.gameObject.tag == "RightBorder" || other.gameObject.tag == "TopBorder")
        {
            foreach (ContactPoint contact in other.contacts)
            {
                //Instantiate your particle system here.
                GameObject part = Instantiate(WallContact, contact.point, Quaternion.identity);
                part.GetComponent<ParticleSystem>().Play();
                StartCoroutine(Flash(m_Material));
                Destroy(part, 3);
            }
        }
    }

    IEnumerator Flash(Material material)
    {
        material.SetColor("_EmissionColor", Color.white);
        yield return new WaitForSeconds(0.4f);
        material.SetColor("_EmissionColor", Color.gray);
    }
}
