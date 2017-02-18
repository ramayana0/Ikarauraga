using UnityEngine;
using System.Collections;

public class bullet_enemy : MonoBehaviour
{

    public Rigidbody2D rb;
    public int speed = 6;
    Vector2 direction;
    Vector2 position;

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        position += direction * speed * Time.deltaTime;
        transform.position = position;
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
    }

    void OnBecameInvisible()
    { 
        Destroy(gameObject);
    }
}