using UnityEngine;
using System.Collections;

public class MoverBoss : Mover {

    private float i = 0;
    private float posX;
    private float posY;
    private float initialPosY;

    // Use this for initialization
    void Start () {
        posX = transform.position.x;
        initialPosY = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        if(posX > 7.8){
            i += 0.02f;
            posX = transform.position.x;
            posX -= 0.016f;
        }
        else
        {
            posX = 7.8f;
        }
        i += 0.02f;
        posY = initialPosY + Mathf.Cos(i);
        Vector2 position = new Vector2(posX, posY);
        transform.position = position;
    }

}
