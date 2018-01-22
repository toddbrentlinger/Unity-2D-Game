using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameControllerV2 : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float startWait;
    public float waveWait;

    public Camera mainCamera;
    public float offset = 0;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    // private bool restart;
    private int score;

    void Start()
    {
        gameOver = false;
        // restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }

    void Update()
    {
        score = Mathf.RoundToInt(transform.position.y / 5);
        UpdateScore();

        if (gameOver)
        {
            restartText.text = "Press 'R' for Restart";
            if (Input.GetKeyDown (KeyCode.R))
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

    public void AddScore (int newScoreValue)
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
