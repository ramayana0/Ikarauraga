using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        Move();
    }

    void Move()
    {
        rb.velocity = transform.right * -speed;
    }

    void OnBecameInvisible()
    {
        // Destroy the enemy
        Destroy(gameObject);
    }
}
