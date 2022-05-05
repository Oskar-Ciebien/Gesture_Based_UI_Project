using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleBehaviour : MonoBehaviour
{
    // == Serialized Fields ==
    [SerializeField] GameObject leftBorder;
    [SerializeField] GameObject rightBorder;

    // == Public Fields ==
    public static Rigidbody rb;
    public static GameObject player;
    public static Vector3 playerPos;
    public GameObject BallContact;
    public GameObject WallContact;
    public static bool frozen = false;

    // == Private Fields ==
    private static Vector2 leftBorderPos;
    private static Vector2 rightBorderPos;
    private Material m_Material;
    private float force = 10f;
    private static int lives;
    private int freezeTime = 2;

    void Start()
    {
        // Initialise the instance
        player = this.gameObject;

        // Set player position
        playerPos = player.transform.position;

        // Set lives
        lives = PlayerPrefs.GetInt("Lives");

        // Set the Components
        rb = player.GetComponent<Rigidbody>();
        m_Material = GetComponent<Renderer>().material;
        m_Material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        // If vuforia connected with image
        if (CustomDefaultTrackableEventHandler.TrueFalse == true)
        {
            // Set the border positions
            leftBorderPos = leftBorder.transform.position;
            rightBorderPos = rightBorder.transform.position;
        }
    }

    public void FrozenPlayer()
    {
        StartCoroutine(PreventMovement());
    }

    IEnumerator PreventMovement()
    {
        // Freeze player
        PaddleBehaviour.frozen = true;

        // Wait for unfreeze
        yield return new WaitForSeconds(freezeTime);

        // Unfreeze player
        PaddleBehaviour.frozen = false;
    }

    public static void Movement(Vector2 touchPosition)
    {
        if (frozen == false)
        {
            // Touched left border
            if (player.transform.position.x < leftBorderPos.x + 2)
            {
                // print("Player collided with left border!");

                // Move player back inside the borders
                playerPos = player.transform.position;
                player.transform.Translate(new Vector3(0, 0.4f, 0));
            }
            // Touched right border
            else if (player.transform.position.x > rightBorderPos.x - 2)
            {
                // print("Player collided with right border!");

                // Move player back inside the borders
                playerPos = player.transform.position;
                player.transform.Translate(new Vector3(0, -0.4f, 0));
            }
            else
            {
                // Player Moving
                playerPos = player.transform.position;
                player.transform.Translate(new Vector2(0, touchPosition.x * Time.deltaTime));
            }
        }
    }

    public static void ResetPlayer()
    {
        // Unfreeze player
        frozen = false;

        // Reset lives
        PlayerPrefs.SetInt("Lives", GameManager.startingLives);

        // Reset Score
        PlayerPrefs.SetInt("Score", GameManager.startingScore);
    }

    public static void Dead()
    {
        // Take away one life
        lives--;

        // Set new lives
        PlayerPrefs.SetInt("Lives", lives);

        // If still enough lives left
        if (lives >= 1 && lives <= 3)
        {
            // Game not started for ball to start off timer
            GameManager.instance.gameStarted = false;

            // Restart Scene
            RestartScene();
        }
        // If no more lives left
        else
        {
            // Game not started for ball to start off timer
            GameManager.instance.gameStarted = false;

            // Set the death scene
            DeathScene();
        }
    }

    // Restart the Game - Player Died (Still has lives)
    public static void RestartScene()
    {
        // Unfreeze player
        frozen = false;

        // Current Scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Load the Scene
        SceneManager.LoadScene(currentScene.name);
    }

    // Show the DeathScene - Player Died (No more lives)
    private static void DeathScene()
    {
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
                Destroy(part, 2.9f);
            }
            SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Clash);
            // Left Side of paddle hit
            if (hitPoint.x < paddleCenter.x)
            {
                // Add force to the ball to the left
                ballRB.AddForce(new Vector3(-BallBehaviour.initialSpeed, 0, Mathf.Abs(difference * force)));
            }
            else // Right side of paddle hit
            {
                // Add force to the ball to the right
                ballRB.AddForce(new Vector3(BallBehaviour.initialSpeed, 0, Mathf.Abs(difference * force)));
            }
        }
    }

    IEnumerator Flash(Material material)
    {
        material.SetColor("_EmissionColor", Color.red);
        yield return new WaitForSeconds(0.2f);
        material.SetColor("_EmissionColor", Color.black);
    }
}
