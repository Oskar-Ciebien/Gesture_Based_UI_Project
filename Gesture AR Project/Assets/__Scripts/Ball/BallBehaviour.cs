using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallBehaviour : MonoBehaviour
{
    // == Serialized Fields ==
    [SerializeField] GameObject objects;

    // == Public Fields ==
    public GameObject WallContact;
    public static float initialSpeed = 10f;

    // == Private Fields ==
    private static GameObject ball;
    private static Rigidbody rb;
    private static float angleMainMenu = 5f;
    private static Quaternion objectsRotation;

    private void Start()
    {
        // This instance
        ball = this.gameObject;

        // Rigidbody
        rb = ball.GetComponent<Rigidbody>();

        // Set object rotation
        objectsRotation = objects.transform.rotation;
    }

    private void FixedUpdate()
    {
        // Constant Same Speed of Ball
        rb.velocity = rb.velocity.normalized * initialSpeed;
    }

    // Start Ball in Game Scene
    public static void StartBall()
    {
        // If game not started
        if (GameManager.instance.gameStarted == false)
        {
            // Set ball rotation
            ball.transform.rotation = objectsRotation;

            // Let the ball free
            rb.isKinematic = false;
            rb.AddForce(new Vector3(0, 0, initialSpeed));

            // Game started
            GameManager.instance.gameStarted = true;

            // print("Game Started!");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // If hit left, top or right border
        if (other.gameObject.tag == "LeftBorder" || other.gameObject.tag == "RightBorder" || other.gameObject.tag == "TopBorder")
        {
            // print("Ball Bounced - Border: " + other.gameObject.tag);

            // Play Sound
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

            // print("Player Dead! Bottom Border hit!");
        }
    }
}
