using UnityEngine;
using System.Collections;

public struct Wave {

    public float startWait;
    public float spawnWait;
    public int nbUnit;
    public GameObject unitType;
    public Vector2 position;

    public Wave(float startWait, float spawnWait, int nbUnit, GameObject unitType, Vector2 position)
    {
        this.startWait = startWait;
        this.spawnWait = spawnWait;
        this.nbUnit = nbUnit;
        this.unitType = unitType;
        this.position = position;
    }
}