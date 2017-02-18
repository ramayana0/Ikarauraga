using UnityEngine;
using System.Collections;

public class BossControlleur : Enemy {

    // Use this for initialization
    void Start () {
        hp = 300;

        GameObject playerObj = GameObject.Find("GameController");
        if (playerObj != null)
        {
            GameController = playerObj.GetComponent<GameController>();
        }
    }

    void OnDestroy()
    {
        GameController.GameFinish();
    }
}
