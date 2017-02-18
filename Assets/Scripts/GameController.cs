using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static int score;

    public GUIText scoreText;
    public GUIText hpText;
    public GUIText gameOverText;
    public GUIText timeText;

    public GameObject enemy;
    public GameObject enemy2;
    public GameObject boss;

    public float difficultyFactor;
    public float comboDelay;

    private int comboMultiplicator;
    private float lastEnemyDestroy;

    private Player player;
    private bool gameOver;
    public bool gameFinish;

    List<Wave> waves = new List<Wave>();


    // Use this for initialization
    void Start()
    {
        gameFinish = false;
        gameOver = false;
        gameOverText.text = "";
        score = 0;
        comboMultiplicator = 1;
        comboDelay = 1.0f;
        lastEnemyDestroy = 0.0f;

        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            player = playerObj.GetComponent<Player>();
        }

        UpdateHp();

        // Waves
        Wave wave;

        #region 1rst wave
        wave = new Wave(3, 0.3f, 10, enemy, new Vector2(10, 0));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy2, new Vector2(10, 1));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy2, new Vector2(10, -1));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy2, new Vector2(10, 1));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy2, new Vector2(10, 1.5f));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy, new Vector2(10, 3));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy, new Vector2(10, -3));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy2, new Vector2(10, 1));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy2, new Vector2(10, 1.5f));
        waves.Add(wave);

        #endregion

        #region 2nd wave

        wave = new Wave(6f, 0.3f, 10, enemy2, new Vector2(10, 0));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy2, new Vector2(10, 1));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy, new Vector2(10, -3));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy2, new Vector2(10, -1));
        waves.Add(wave);

        wave = new Wave(5, 0.3f, 10, enemy, new Vector2(10, 3));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy, new Vector2(10, -3));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy2, new Vector2(10, 1));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy, new Vector2(10, 3));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy, new Vector2(10, -3));
        waves.Add(wave);

        #endregion

        #region 3rd wave

        wave = new Wave(6, 0.3f, 15, enemy, new Vector2(10, -2));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy, new Vector2(10, 2));
        waves.Add(wave);

        wave = new Wave(0, 0.3f, 10, enemy2, new Vector2(10, 3));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy, new Vector2(10, 0));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 15, enemy2, new Vector2(10, -1));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy, new Vector2(10, -2));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 15, enemy2, new Vector2(10, -1));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy, new Vector2(10, 2));
        waves.Add(wave);

        wave = new Wave(0.5f, 0.3f, 10, enemy2, new Vector2(10, 1));
        waves.Add(wave);

        #endregion

        wave = new Wave(10, 0.3f, 1, boss, new Vector2(10, 0));
        waves.Add(wave);

        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameOver)
            timeText.text = Time.timeSinceLevelLoad.ToString();

        resetComboMultiplicator();

        if (gameOver)
        {
            if (Input.GetButton("Submit"))
            {
                SceneManager.LoadScene("mainScene");
            }

            if (Input.GetButton("Cancel"))
                SceneManager.LoadScene("mainMenu");
        }
    }

    public void AddScore(int newScoreValue)
    {
        comboMultiplicator++;
        lastEnemyDestroy = Time.time;
        score += newScoreValue * comboMultiplicator;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score + @"
        Multi x " + comboMultiplicator;

    }

    public void resetComboMultiplicator()
    {
        if (Time.time > comboDelay + lastEnemyDestroy)
        {
            comboMultiplicator = 1;
            UpdateScore();
        }
    }

    public void UpdateHp()
    {
        hpText.text = "HP: " + player.Hp;
    }

    IEnumerator SpawnWaves()
    {
        foreach (Wave wave in waves)
        {
            yield return new WaitForSeconds(wave.startWait * difficultyFactor);

            float deltaX = 0;
            for (int i = 0; i < wave.nbUnit; i++)
            {
                deltaX += 0.5f;
                SpawnUnit(new Vector2(wave.position.x + deltaX, wave.position.y), wave.unitType);
            }

            if (gameOver)
            {
                break;
            }
        }
    }

    void SpawnUnit(Vector3 spawnValues, GameObject Unit)
    {
        Vector3 spawnPosition = new Vector3(spawnValues.x, spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = transform.rotation;
        Instantiate(Unit, spawnPosition, spawnRotation);
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "Game Over\nPress ENTER to restart the game and ESCAPE to exit";
        timeText.text = Time.timeSinceLevelLoad.ToString();
    }

    public void GameFinish()
    {
        gameFinish = true;
        gameOverText.text = "Game Finish\nPress ENTER to restart the game and ESCAPE to exit";
        timeText.text = Time.timeSinceLevelLoad.ToString();
    }
}
