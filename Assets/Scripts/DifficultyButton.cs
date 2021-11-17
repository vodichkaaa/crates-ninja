using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button _button;
    private GameManager _gameManager;

    public int difficulty;
    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        _button = GetComponent<Button>();
        _button.onClick.AddListener(GameDifficulty);
    }

    private void GameDifficulty()
    {
        _gameManager.StartGame(difficulty);

    }

}
