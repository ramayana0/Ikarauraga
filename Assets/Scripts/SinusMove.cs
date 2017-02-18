using UnityEngine;
using System.Collections;

public class SinusMove : Mover {

    private float i = 0;
    private float posX;
    private float posY;
    private float initialPosY;

    // Use this for initialization
    void Start () {
        initialPosY = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
    }

    void Move()
    {
        i+=0.01f;
        posX = transform.position.x;
        posX-=0.016f;
        posY = initialPosY + Mathf.Cos(i);
        Vector2 position = new Vector2(posX, posY);
        transform.position = position;
    }
}
