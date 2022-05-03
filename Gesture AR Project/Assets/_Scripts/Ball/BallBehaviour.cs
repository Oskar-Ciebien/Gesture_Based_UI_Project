using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] GameObject objects;
    // == Public Fields ==
    public GameObject WallContact;
    public static float initialSpeed = 5f;

    // == Private Fields ==
    private Vector3 paddlePos;
    private static Vector3 startingPos;
    private static GameObject ball;
    private static Rigidbody rb;
    private static float angleMainMenu = 5f;
    private static Quaternion objectsRotation;

    private void Start()
    {
        ball = this.gameObject;

        rb = ball.GetComponent<Rigidbody>();

        objectsRotation = objects.transform.rotation;
    }

    private void Update()
    {
        // Constant Same Speed of Ball
        rb.velocity = rb.velocity.normalized * initialSpeed;
    }

    public static void StartBall()
    {
        // If game not started
        if (GameManager.gameStarted == false)
        {
            ball.transform.rotation = objectsRotation;

            // Let the ball free
            rb.isKinematic = false;
            rb.AddForce(new Vector3(initialSpeed, 0, 0));

            // Game started
            GameManager.gameStarted = true;

            print("Game Started!");
        }
    }

    // Start Ball in Main Menu
    public static void StartBallMenu()
    {
        // If game not started and on main menu scene
        if (GameManager.gameStarted == false && SceneManager.GetActiveScene().name == "Main Menu")
        {
            // Let the ball free on an angle
            rb.isKinematic = false;
            rb.AddForce(new Vector2(angleMainMenu, initialSpeed));

            print("Ball Started off in Main Menu!");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // If hit left, top or right border
        if (other.gameObject.tag == "LeftBorder" || other.gameObject.tag == "RightBorder" || other.gameObject.tag == "TopBorder")
        {
            // Bounce off the border
            print("Ball Bounced - Border: " + other.gameObject.tag);
            SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Bounce);
            foreach (ContactPoint contact in other.contacts)
            {
                //Instantiate your particle system here.
                GameObject part = Instantiate(WallContact, contact.point, Quaternion.identity);
                part.GetComponent<ParticleSystem>().Play();
                Destroy(part, 3);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If hit bottom border
        if (other.gameObject.tag == "BottomBorder")
        {
            // Player Dead
            PaddleBehaviour.Dead();

            print("Player Dead! Bottom Border hit!");
        }
    }
}
