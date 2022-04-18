using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static GameObject player;
    [SerializeField] float speed = 20f;
    Vector3 direction = new Vector3(1, 4);

    Rigidbody rb;

    void Start()
    {
        player = this.gameObject;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        player.transform.Translate(new Vector3(0, Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0));
    }

    public static void Die()
    {
        print("Player Died");
    }
}
