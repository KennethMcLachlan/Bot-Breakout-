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
    [SerializeField] public Transform _spawnPoint;

    //Lists
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private List<GameObject> _enemyPool;

    [SerializeField] private GameObject _enemyContainer;

    private int _spawnCount = 10;

    private bool _enemiesCanSpawn;

    private bool _roundOver;

    //[SerializeField] private int _waveCount;


    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _spawnCount = 10;
        GenerateEnemies(1);
        _enemiesCanSpawn = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            BeginSpawning();
        }
    }

    public void BeginSpawning()
    {
        StartCoroutine(EnemySpawnRoutine());
    }

    private IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_enemiesCanSpawn == true)
        {

            for (int enemyCount = _spawnCount; enemyCount > 0; enemyCount--)
            {
                RequestEnemy();
                

                yield return new WaitForSeconds(4f);

                if (enemyCount <= 0)
                {
                    //End Round
                    break;
                }
            }

            _enemiesCanSpawn = false;
        }
    }

    //private void InstantiateEnemy()
    //{
    //    Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
    //    _spawnCount++;
    //}

    private List<GameObject> GenerateEnemies(int amountOfEnemies)
    {
        for (int i = 0; i < amountOfEnemies; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            enemy.SetActive(false);
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
                enemy.SetActive(true);
                _spawnCount++;
                return enemy;
            }
        }

        GameObject newEnemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
        _enemyPool.Add(newEnemy);

        return newEnemy;
    }

    public List<Transform> SendWaypoints()
    {
        return _waypoints;
    }

    private void RoundReset()
    {
        if (_enemiesCanSpawn == false)
        {
            _spawnCount = _spawnCount + 10;
            StartCoroutine(WaveSetRoutine());
            //Start Wave Build Up Coroutine
        }
    }

    IEnumerator WaveSetRoutine()
    {
        //UI and timing to explain the incoming round
        //Get ready Go!
        yield return new WaitForSeconds(8f);
        _enemiesCanSpawn = true;
    }
}
