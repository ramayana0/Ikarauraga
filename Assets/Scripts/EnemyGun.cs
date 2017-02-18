using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour {

    public GameObject enemyBullet;
    public float fireRate;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Fire", 0, fireRate);
    }

    void Fire()
    {
        //  Reference to the player's ship;
        GameObject playerShip = GameObject.Find("Player");

        if(playerShip != null)
        {
            // Instantiate an enemy bullet
            GameObject bullet = (GameObject)Instantiate(enemyBullet);

            // Set the bullet's initial position
            bullet.transform.position = transform.position;

            // Compute the bullet direction toward the player's ship
            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            // Set the bullet direction
            bullet.GetComponent<bullet_enemy>().SetDirection(direction);            
        }
    }
}
