using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is NULL");
            }
            return _instance;
        }
    }

    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _enemyTotal;
    [SerializeField] private Slider _wallHeath;

    //Game Over Sequence
    [SerializeField] private GameObject _gameOverUI;

    private void Awake()
    {
        {
            _instance = this;   
        }
    }

    public void UpdateScore(int playerScore)
    {
        //Make a switch statement to update the score depending on which enemy was defeated
        Debug.Log("Updating Score: " + playerScore);
        _score.text = playerScore.ToString();
    }

    public void UpdateEnemyCount(int enemyCount)
    {
        Debug.Log("Updating Enemy Count: " + enemyCount);
        _enemyTotal.text = enemyCount.ToString();
    }

    public void UpdateWallHealth(int health)
    {
        _wallHeath.value = health;
    }

    public void GameOverSequence()
    {
        _gameOverUI.SetActive(true);
    }
}
