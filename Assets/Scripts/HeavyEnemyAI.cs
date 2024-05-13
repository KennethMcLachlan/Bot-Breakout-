using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeavyEnemyAI : MonoBehaviour
{
    private enum AIState
    {
        Idle,
        Walking,
        Running,
        Death
    }

    private List<Transform> _waypoints;
    [SerializeField] private AIState _currentState;

    private NavMeshAgent _agent;
    private int _currentPoint = 0;
    private bool _inReverse;

    //Enemy Health
    private int _maxHealth = 10;

    private int _points;

    private void Start()
    {
        _maxHealth = 10;
        _waypoints = SpawnManager.Instance.SendWaypoints();

        _agent = GetComponent<NavMeshAgent>();
        if (_agent != null)
        {
            _agent.destination = _waypoints[_currentPoint].position;
        }
    }

    private void Update()
    {
        CalculateMovement();

        if (_maxHealth <= 0)
        {
            HeavyEnemyDeath();
        }
    }

    private void CalculateMovement()
    {
        if (_agent.remainingDistance < 0.5f)
        {
            if (_inReverse == true)
            {
                Reverse();
            }
            else
            {
                Forward();
            }

            _agent.SetDestination(_waypoints[_currentPoint].position);
        }
    }

    private void Forward()
    {
        if (_currentPoint == _waypoints.Count - 1)
        {
            _inReverse = true;
            _currentPoint--;
        }
        else
        {
            _currentPoint++;
        }
    }

    private void Reverse()
    {
        if (_currentPoint == 0)
        {
            _inReverse = false;
            _currentPoint++;
        }
        else
        {
            _currentPoint--;
        }
    }

    public void WaypointReceiver()
    {
        SpawnManager.Instance.SendWaypoints();
    }

    public void HeavyEnemyDeath()
    {
        EnemyDeathPoint(500);
        EnemyReposition();
        Debug.Log("Heavy Enemy Has Died");
    }

    public void Damage()
    {
        _maxHealth -= 10;
    }

    public void SelfDestruct()
    {
        EnemyReposition();
    }

    public void EnemyReposition()
    {
        this.gameObject.SetActive(false);
        this.gameObject.transform.position = SpawnManager.Instance._spawnPoint.position;
    }

    public void EnemyDeathPoint(int points)
    {
        _points += points;

        UIManager.Instance.UpdateScore(_points);
    }
}
