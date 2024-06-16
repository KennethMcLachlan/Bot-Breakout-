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
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private TMP_Text _warningText;
    [SerializeField] private Slider _wallHealth;

    [SerializeField] private Animator _warningTextAnim;

    private int _waveNumber = 0;
    private float _oneSecond = 1f;

    //Game Over Sequence
    [SerializeField] private GameObject _gameOverUI;

    private void Awake()
    {
        {
            _instance = this;   
        }
    }

    private void Start()
    {
        _waveText.text = "";

        _warningTextAnim = GetComponentInChildren<Animator>();
        if (_warningTextAnim == null)
        {
            Debug.LogError("Warning Text Animator is NULL");
        }

    }

    private void Update()
    {
        
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
        _wallHealth.value = health;

        if (health > 75)
        {
            _warningTextAnim.SetBool("LowHealth", false);
            Debug.Log("Wall Integrity is greater than half");
        }

        if (health <= 75)
        {
            _warningTextAnim.SetBool("LowHealth", true);
            Debug.Log("Wall Integrity is below half");
        }

        if (health <= 25)
        {
            _warningText.GetComponent<TMP_Text>().color = Color.red;
        }
    }

    private void IncreaseWallIntegrity(float percentage)
    {
        float currentHealth = _wallHealth.value;
        float increaseHealth = currentHealth * percentage / 100f;
        _wallHealth.value = currentHealth + increaseHealth;

        UpdateWallHealth((int)_wallHealth.value);
    }

    public void GameOverSequence()
    {
        _gameOverUI.SetActive(true);
    }

    public void UpdateWaves()
    {
        _waveNumber += 1;
        Debug.Log("UpdateWaves has been called on in the UI Manager");
        IncreaseWallIntegrity(30f);
        StartCoroutine(WaveCountdownRoutine());
    }

    IEnumerator WaveCountdownRoutine()
    {
        _waveText.text = "Wave " + _waveNumber.ToString();
        
        yield return new WaitForSeconds(3f);

        _waveText.text = "3";
        yield return new WaitForSeconds(_oneSecond);

        _waveText.text = "2";
        SpawnManager.Instance.StartEnemySpawn();
        yield return new WaitForSeconds(_oneSecond);

        _waveText.text = "1";
        yield return new WaitForSeconds(_oneSecond);

        _waveText.text = "";
    }
}
