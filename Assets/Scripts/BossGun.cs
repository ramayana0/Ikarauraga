using UnityEngine;
using System.Collections;

public class BossGun : EnemyGun {

    public GameObject bossBullet;
    public GameObject enemy2Bullet;
    private GameObject playerShip;

    int i = 0;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Fire", 0, fireRate);
        playerShip = GameObject.Find("Player");
    }

    void Fire()
    {
        //  Reference to the player's ship;
        

        if (playerShip != null)
        {
            i++;
            if(i == 2){
                Fire2(bossBullet);
                i = 0;
            }
            
            Fire2(enemyBullet);
            Fire2(enemy2Bullet);
        }
    }

    void Fire2(GameObject enemyBullet)
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
