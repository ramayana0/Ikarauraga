using UnityEngine;
using System.Collections;

public abstract class Aircraft : MonoBehaviour {

    public Rigidbody2D rb;
    public Renderer rd;
    public GameObject bullet;
    public Transform bulletSpawn;
    public GameObject explosionGO;
    public int hp;
    public float fireRate;
    public int speed;

    protected float lastShot;

    public int Hp
    {
        get { return hp; }
    }

    // Use this for initialization
    void Start()
    {
        lastShot = 0.0f;
    }    
}
