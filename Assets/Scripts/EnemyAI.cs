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

    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private AIState _currentState;

    private NavMeshAgent _agent;
    private int _currentPoint = 0;
    private bool _inReverse;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent != null)
        {
            _agent.destination = _waypoints[_currentPoint].position;
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
}
