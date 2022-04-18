using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        transform.parent = null;
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "LeftBorder" || other.gameObject.tag == "RightBorder" || other.gameObject.tag == "TopBorder")
        {
            // Bounce
            print("Ball Bounced, Border: " + other.gameObject.tag);
        }
        else if (other.gameObject.tag == "BottomBorder")
        {
            // Player Dead
            PlayerBehaviour.Die();
        }
    }
}
