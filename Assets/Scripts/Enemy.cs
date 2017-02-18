using UnityEngine;
using System.Collections;

public class Enemy : Aircraft
{
    protected GameController GameController;
    public int scoreValue;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.Find("GameController");
        if (playerObj != null)
        {
            GameController = playerObj.GetComponent<GameController>();
        }   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerB_Tag")
        {
            hp--;
            Destroy(other.gameObject);
        }

        if (other.tag == "Player")
        {
            hp--;
        }

        if (other.tag == "PlayerB_Tag" && hp == 0)
        {
            Instantiate(explosionGO, other.transform.position, other.transform.rotation);
            Destroy(gameObject);
            GameController.AddScore(scoreValue);
        }
        if (other.tag == "Player" && hp == 0)
        {
            Instantiate(explosionGO, other.transform.position, other.transform.rotation);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        // Destroy the enemy
        Destroy(gameObject);
    }
}
