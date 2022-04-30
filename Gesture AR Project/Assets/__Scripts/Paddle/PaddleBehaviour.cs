using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaddleBehaviour : MonoBehaviour
{
    public static GameObject player;
    public static Vector3 playerPos;
    public GameObject BallContact;
    public GameObject WallContact;

    private Rigidbody rb;
    private Material m_Material;
    private static float leftWallPos = -10f;
    private static float rightWallPos = 10f;
    private float force = 10f;

    void Start()
    {
        player = this.gameObject;

        rb = player.GetComponent<Rigidbody>();
        m_Material = GetComponent<Renderer>().material;
        m_Material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        // Movement();
    }

    public static void Movement(Vector2 touchPosition)
    {
        if (playerPos.x < leftWallPos)
        {
            print("Player collided with a wall");

            playerPos = player.transform.position;
            player.transform.Translate(new Vector3(0, 0.1f, 0));
        }
        else if (playerPos.x > rightWallPos)
        {
            print("Player collided with a wall");

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
            foreach (ContactPoint contact in other.contacts)
            {
                //Instantiate your particle system here.
                GameObject part = Instantiate(BallContact, contact.point, Quaternion.identity);
                part.GetComponent<ParticleSystem>().Play();
                StartCoroutine(Flash(m_Material));
                Destroy(part, 3);

            }

            // Left
            if (hitPoint.x < paddleCenter.x)
            {
                ballRB.AddForce(new Vector2(-(Mathf.Abs(difference * force)), BallBehaviour.initialSpeed));

            }
            else // Right
            {
                ballRB.AddForce(new Vector2((Mathf.Abs(difference * force)), BallBehaviour.initialSpeed));
            }
        }

        if (other.gameObject.tag == "LeftBorder" || other.gameObject.tag == "RightBorder")
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
