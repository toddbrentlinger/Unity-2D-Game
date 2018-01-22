using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerV3 : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float startWait, waveWait;

    public Camera mainCamera;
    public float offset = 0;

    public Text scoreText, restartText, gameOverText;

    private bool gameOver;
    // private bool restart;
    private int score, tempScore;

    void Awake()
    {
        gameOver = false;
        // restart = false;
        score = 0;
        tempScore = 0;

        restartText.text = "";
        gameOverText.text = "";
    }

    void Start()
    {
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        tempScore = Mathf.RoundToInt(transform.position.y / 5);
        if (tempScore > score)
            score = tempScore;
        UpdateScore();

        if (gameOver)
        {
            restartText.text = "Press 'R' for Restart";
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene("Flappy Bird V2");
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; ++i)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(
                    Random.Range(-spawnValues.x, spawnValues.x),
                    spawnValues.y,
                    spawnValues.z
                    );
                Quaternion spawnRotation = Quaternion.identity;

                spawnPosition += transform.position; // for attached object world position
                GameObject lastPrefab = Instantiate(hazard, spawnPosition, spawnRotation) as GameObject;

                float delta = 0;
                do
                {
                    float spawnHeight = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0f)).y;
                    delta = spawnHeight - lastPrefab.transform.position.y;
                    yield return null;
                } while (delta < offset && lastPrefab != null);
            }

            if (gameOver)
                break;

            yield return new WaitForSeconds(waveWait);
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
