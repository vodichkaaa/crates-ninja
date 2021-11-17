using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI gameOverText;
    [SerializeField]
    private TextMeshProUGUI livesText;
    [SerializeField]
    private GameObject restartButton;
    [SerializeField]
    private GameObject titleScreen;
    [SerializeField]
    private GameObject pauseScreen;

    private int _score;

    private float _spawnRate = 1f;

    public bool isGameActive;

    private bool _paused;

    public int health = 5;

    private void Update()
    {
        Health();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

    }

    private IEnumerator SpawnTargets()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int addScore)
    {
        _score += addScore;
        scoreText.text = "Score: " + _score;
    }

    private void GameOver()
    {
        scoreText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(false);
        restartButton.SetActive(true);

        isGameActive = false;
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;

        StartCoroutine(SpawnTargets());

        _score = 0;
        UpdateScore(0);
        titleScreen.SetActive(false);

        _spawnRate /= difficulty;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Health()
    {
        livesText.text = "Lives: " + health;

        if(health <= 0)
        {
            GameOver();
        }
    }

    private void Pause()
    {
        if(!_paused)
        {
            _paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;

        }
        else
        {
            _paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
