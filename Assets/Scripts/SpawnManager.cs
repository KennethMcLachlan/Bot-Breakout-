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

    private bool _enemiesCanSpawn;

    private bool _roundOver;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _currentSpawnCount = _initialSpawnCount;
        GenerateEnemies(_currentSpawnCount);
        _enemiesCanSpawn = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //Will be changed to a different starter case
        {
            StartCoroutine(EnemySpawnRoutine());
        }
    }

    private IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_enemiesCanSpawn == true)
        {
            
            for (int i = 0; i < _currentSpawnCount; i++)
            {
                RequestEnemy();
                yield return new WaitForSeconds(1.5f);
            }

            _enemiesCanSpawn = false;
            StartCoroutine(WaveSetRoutine());
            Debug.Log("Start Wave Set Routine");
        }
    }


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

    //public GameObject RequestEnemy()
    //{
    //    foreach (var enemy in _enemyPool)
    //    {
    //        if (enemy.activeInHierarchy == false)
    //        {
    //            InitializeEnemy(enemy);
    //            return enemy;
    //        }
    //    }

    //    GameObject newEnemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
    //    InitializeEnemy(newEnemy);
    //    _enemyPool.Add(newEnemy);
    //    return newEnemy;
    //}

    //private void InitializeEnemy(GameObject enemy)
    //{
    //    enemy.transform.position = _spawnPoint.position;
    //    enemy.transform.rotation = Quaternion.identity;
    //    enemy.SetActive(true);
    //    EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
    //    if (enemyAI != null)
    //    {
    //        enemyAI.ResetAI();
    //    }
    //    else
    //    {
    //        Debug.LogError("EnemyAI component not found");
    //    }
    //}

    public GameObject RequestEnemy()
    {
        foreach (var enemy in _enemyPool)
        {
            if (enemy.activeInHierarchy == false)
            {
                enemy.transform.position = _spawnPoint.position;
                enemy.transform.rotation = Quaternion.identity;
                enemy.SetActive(true);
                //enemy.GetComponent<EnemyAI>().ResetAI();
                return enemy;
            }
        }

        GameObject newEnemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
        newEnemy.SetActive(true);
        //newEnemy.GetComponent<EnemyAI>().ResetAI();
        _enemyPool.Add(newEnemy);
        return newEnemy;
    }

    public List<Transform> SendWaypoints()
    {
        return _waypoints;
    }

    IEnumerator WaveSetRoutine()
    {
        //UI and timing to explain the incoming round
        //Get ready Go!
        yield return new WaitForSeconds(8f);
        _currentSpawnCount = Mathf.CeilToInt(_currentSpawnCount * _spawnMultiplier);
        _enemiesCanSpawn = true;
        StartCoroutine(EnemySpawnRoutine());
        Debug.Log("WaveSetRoutine Complete. Next wave will spawn: " + _currentSpawnCount + "enemies.");
    }
}
