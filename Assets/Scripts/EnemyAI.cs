using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private enum AIState
    {
        Idle,
        Walking,
        Running,
        Death
    }

    private List<Transform> _wayPoints;
    [SerializeField] private AIState _currentState;

    private NavMeshAgent _agent;
    private int _currentPoint = 0;
    private bool _inReverse;

    //Heavy Enemy Health

    private int _points;
    private int _enemyPoints;

    private void Start()
    {
        _wayPoints = SpawnManager.Instance.SendWaypoints();

        _agent = GetComponent<NavMeshAgent>();
        if (_agent != null)
        {
            _agent.destination = _wayPoints[_currentPoint].position;
        }
        
    }

    private void Update()
    {
        CalculateMovement();
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

            _agent.SetDestination(_wayPoints[_currentPoint].position);
        }
    }
    private void Forward()
    {
        if (_currentPoint == _wayPoints.Count - 1)
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

    public void Damage()
    {
        SendPoints(100);
        SendEnemyCount(1);
        EnemyReposition();
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

    public void SendPoints(int points)
    {
        _points += points;
        
        UIManager.Instance.UpdateScore(_points);
    }

    public void SendEnemyCount(int points)
    {
        _enemyPoints += points;
        UIManager.Instance.UpdateEnemyCount(_enemyPoints);
    }
}
