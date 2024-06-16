using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;

    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SpawnManager is NULL");
            }
            return _instance;
        }
    }

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _heavyEnemyPrefab;
    [SerializeField] public Transform _spawnPoint;

    //Lists
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private List<GameObject> _enemyPool;

    [SerializeField] private GameObject _enemyContainer;

    [SerializeField] private int _initialSpawnCount = 3;
    [SerializeField] private int _currentSpawnCount;
    [SerializeField] private float _spawnMultiplier = 0.5f;

    private bool _enemiesAreActive;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _currentSpawnCount = _initialSpawnCount;
        GenerateEnemies(_currentSpawnCount);
        //_enemiesCanSpawn = true; //Temporary. Needs to be changed when new starter case is made
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //Will be changed to a different starter case
        {
            UIManager.Instance.UpdateWaves();
        }

    }

    private IEnumerator EnemySpawnRoutine()
    {
        //_enemiesCanSpawn = true;
        _enemiesAreActive = true;
        yield return new WaitForSeconds(3.0f);

        for (int i = 0; i < _currentSpawnCount; i++)
        {
            RequestEnemy();

            float randomSpawnTime = Random.Range(0.5f, 2.5f);
            yield return new WaitForSeconds(randomSpawnTime);

        }

        
        while (_enemiesAreActive == true)
        {
            EnemyPoolActivity();
            yield return null;
        }
        
        Debug.Log("Enemy Spawn has broke out of the FOR LOOP");
    }

    public void StartEnemySpawn()
    {
        _currentSpawnCount = Mathf.CeilToInt(_currentSpawnCount * _spawnMultiplier);
        StartCoroutine(EnemySpawnRoutine());
    }

    private List<GameObject> GenerateEnemies(int amountOfEnemies)
    {
        for (int i = 0; i < amountOfEnemies; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            enemy.SetActive(false);
            Debug.Log("Enemy.SetActive(false);");
            _enemyPool.Add(enemy);
        }

        return _enemyPool;
    }

    public GameObject RequestEnemy()
    {
        foreach (var enemy in _enemyPool)
        {
            if (enemy.activeInHierarchy == false)
            {
                enemy.transform.position = _spawnPoint.position;
                enemy.transform.rotation = Quaternion.identity;
                enemy.SetActive(true);

                return enemy;
            }
        }

        GameObject newEnemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
        newEnemy.SetActive(true);
        _enemyPool.Add(newEnemy);
        return newEnemy;
    }

    public List<Transform> SendWaypoints()
    {
        return _waypoints;
    }

    private void EnemyPoolActivity()
    {
        _enemiesAreActive = false;
        foreach (var enemy in _enemyPool)
        {
            if (enemy.activeInHierarchy == true)
            {
                _enemiesAreActive = true;
                break;
            }
        }

        if (_enemiesAreActive == false)
        {
            UIManager.Instance.UpdateWaves();
        }
    }
}
