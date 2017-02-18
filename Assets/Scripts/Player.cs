using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class Player : Aircraft
{
    public Boundary boudary;
    public float invulnerabiltyDelay;

    private GameController GameController;
    public GameObject aura;
    
    public Sprite Square;
    public Sprite Circle;
    private List<Sprite> shapes = new List<Sprite>();
    private SpriteRenderer srAura;

    private bool shieldSquare;
    private bool isNotProtected;

    private float lastHit = 0.0f;
    private int i = 0;

    private float posX;
    private float posY;

    private int power = 1;

    // Use this for initialization
    void Start()
    {
        hp = 1;
        rb = GetComponent<Rigidbody2D>();
        rd = GetComponent<Renderer>();

        shieldSquare = false;

        aura = GameObject.Find("Aura");
        srAura = aura.GetComponent<SpriteRenderer>();

        GameObject playerObj = GameObject.Find("GameController");
        if (playerObj != null)
        {
            GameController = playerObj.GetComponent<GameController>();
        }
        shapes.Add(Square);
        shapes.Add(Circle);
    }

    void Update()
    {
        // When the spacebar is pressed
        if (Input.GetButton("Fire"))
        {
            Fire();
        }

        if (Input.GetButtonDown("Switch shield"))
        {
            changeShield();
        }
    }

    private void changeShield()
    {
        srAura.sprite = shapes[i];
        Debug.Log("shape change");
 
        shieldSquare = !shieldSquare;

        if (i == 0) i = 1;
        else if (i == 1) i = 0;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
            (
            Mathf.Clamp(rb.position.x, boudary.xMin, boudary.xMax),
            Mathf.Clamp(rb.position.y, boudary.yMin, boudary.yMax),
            0.0f
            );
    }

    private void Fire()
    {

        if (Time.time > fireRate + lastShot)
        {
            if (power == 1)
            {
                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

            }
            else
            {
                float posX = bulletSpawn.position.x;
                float posY = bulletSpawn.position.y;

                Instantiate(bullet, new Vector2(posX, posY - 0.1f), bulletSpawn.rotation);
                Instantiate(bullet, new Vector2(posX, posY + 0.1f), bulletSpawn.rotation);
            }
            lastShot = Time.time;
        }
    }

    // OnTriggerEnter2D si called when a colision occur.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PowerUp")
        {
            power++;
            Destroy(other.gameObject);
        }

        if (other.tag == "Enemy")
        {
            hp = 0;
            GameController.UpdateHp();
        }

        isNotProtected = other.tag != "EnemyBulletPinkSquare" && shieldSquare || other.tag != "EnemyBulletGreenCircle" && !shieldSquare;

        if (isNotProtected && other.tag != "PowerUp" && other.tag != "PlayerB_Tag")
        {
            StartCoroutine("hit");
            GameController.UpdateHp();
            Destroy(other.gameObject);
        }

        if (other.tag == "Enemy" && hp <= 0 || (isNotProtected && other.tag != "PowerUp" && other.tag != "PlayerB_Tag") && hp <= 0)
        {
            Instantiate(explosionGO, other.transform.position, other.transform.rotation);
            Destroy(gameObject);
            GameController.GameOver();
        }
    }

    private IEnumerator hit()
    {
        GameController.resetComboMultiplicator();
        if (Time.time > lastHit + invulnerabiltyDelay) {
            hp--;
            lastHit = Time.time;
        }
        rd.material.color = new UnityEngine.Color(1, 1, 1, 0.5f); // Set opacity to 50%
        yield return new WaitForSeconds(invulnerabiltyDelay);
        rd.material.color = new UnityEngine.Color(1, 1, 1, 1); // Set opacity to 100%
    }
}