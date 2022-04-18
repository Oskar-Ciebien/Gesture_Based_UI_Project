using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    Vector3 direction = new Vector3(1, 4);

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0);

        if (transform.childCount > 0)
        {
            BallBehaviour ball = GetComponentInChildren<BallBehaviour>();
            ball.Move(direction);
        }
    }

    public static void Die()
    {
        print("Player Died");
    }
}
