using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //AI & Waypoints
    private enum AIState
    {
        Walking,
        Running,
        Hiding,
        Death
    }

    private List<Transform> _wayPoints;
    [SerializeField] private AIState _currentState;
    private NavMeshAgent _agent;
    private int _currentPoint = 0;
    private bool _inReverse;

    private Animator _animator;

    //Enemy Death Explosion
    private GameObject _botExplosionContainer;
    private ParticleSystem _botExplosion;
    private SkinnedMeshRenderer _botMeshRenderer;
    private AudioSource _botDeathSFX;

    //Enemy Death Tracking
    private int _totalPoints;
    private int _totalEnemiesDestroyed;

    //Enemy Hiding Timer
    private float _hideTimer = 0f;
    [SerializeField] private float _hideDuration = 3f;

    //AI State Bool Set
    private bool _isWalking;
    private bool _isRunning;
    private bool _isHiding;
    private bool _isDead;

    private void Start()
    {
        _wayPoints = SpawnManager.Instance.SendWaypoints();
        if (_wayPoints == null || _wayPoints.Count == 0)
        {
            Debug.LogError("Waypoints are not set or empty");
            return;
        }

        _agent = GetComponent<NavMeshAgent>();
        if (_agent != null)
        {
            _agent.destination = _wayPoints[_currentPoint].position;
        }

        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
        {
            Debug.Log("Animator is NULL");
        }

        _botExplosionContainer = transform.Find("BotExplosion").gameObject;
        _botExplosion = _botExplosionContainer.GetComponentInChildren<ParticleSystem>();
        if (_botExplosion == null)
        {
            Debug.Log("BotExplosion is NULL");
        }

        Transform botModel = transform.Find("Enemy_Robot/Mech_017");
        _botMeshRenderer = botModel.GetComponentInChildren<SkinnedMeshRenderer>();
        if (_botMeshRenderer == null)
        {
            Debug.Log("Bot Mesh Renderer is NULL");
        }

        _botDeathSFX = GetComponentInChildren<AudioSource>();
        if (_botDeathSFX == null)
        {
            Debug.Log("Bot Death Explosion SFX is NULL");
        }

        _currentState = AIState.Walking;
    }

    private void Update()
    {

        //Determine Current AI Behavior
        switch (_currentState)
        {

            case AIState.Walking:
                Debug.Log("Enemy Is Walking");
                _animator.SetBool("Hiding", false);
                _animator.SetFloat("Speed", 4.9f);
                _agent.speed = 4.9f;

                //CalculateMovement();
                break;

            case AIState.Running:
                Debug.Log("Enemy is Running");
                _animator.SetBool("Hiding", false);
                _animator.SetFloat("Speed", 10f);
                _agent.speed = 10;

                //CalculateMovement();
                break;

            case AIState.Hiding:
                Debug.Log("Enemy is Hiding");
                _animator.SetBool("Hiding", true);
                _animator.SetFloat("Speed", 0f);
                _agent.speed = 0;

                //CalculateMovement();
                break;

            case AIState.Death:
                Debug.Log("Enemy is Playing Death Anim");
                _animator.SetBool("Hiding", false);
                _agent.speed = 0;
                break;
        }

        //Hide State Timer
        if (_currentState == AIState.Hiding)
        {
            _hideTimer += Time.deltaTime;

            if (_hideTimer >= _hideDuration)
            {
                RandomizeAIState();
                _hideTimer = 0;
            }
        }
    }

    //private void CalculateMovement()
    //{
    //    if (_agent.remainingDistance < 0.5f)
    //    {
    //        RandomizeAIState();

    //        _currentPoint = (_currentPoint + 1) % _wayPoints.Count;
    //        _agent.SetDestination(_wayPoints[_currentPoint].position);
    //    }

    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            Debug.Log("Waypoint Trigger has been hit!");
            _currentPoint = (_currentPoint + 1) % _wayPoints.Count;
            _agent.SetDestination(_wayPoints[_currentPoint].position);
            RandomizeAIState();
        }
    }
    private void RandomizeAIState()
    {
        AIState[] availableStates = { AIState.Walking, AIState.Running, AIState.Hiding };
        _currentState = availableStates[Random.Range(0, availableStates.Length)];
        _agent.SetDestination(_wayPoints[_currentPoint].position);
    }

    //public void ResetAI()
    //{
    //    _currentState = AIState.Walking;
    //    _currentPoint = 0;
    //    _agent.SetDestination(_wayPoints[_currentPoint].position);
    //    _animator.SetBool("Hiding", false);
    //    _animator.SetFloat("Speed", 4.9f);
    //    _agent.speed = 4.9f;
    //    _botMeshRenderer.enabled = true;
    //}
    public void WaypointReceiver()
    {
        SpawnManager.Instance.SendWaypoints();
    }

    public void Damage()
    {
        _animator.SetTrigger("Death");
        _currentState = AIState.Death;
        //Invoke("ExplodingBot", 3);
        StartCoroutine(BotDeathSequence());
        //_botExplosion.Play();
        SendPoints(100);
        SendEnemyCount(1);
    }

    //private void ExplodingBot()
    //{
    //    _botExplosion.Play();
    //    Invoke("EnemyReposition", 3f);
    //}

    private IEnumerator BotDeathSequence()
    {
        //yield return new WaitForSeconds(2.25f);
        yield return new WaitForSeconds(3f);
        _botDeathSFX.Play();
        _botExplosion.Play();
        yield return new WaitForSeconds(1f);

        _botMeshRenderer.enabled = false;

        yield return new WaitForSeconds(4f);
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
        //ResetAI();
        this.gameObject.SetActive(true);
        //_botMeshRenderer.enabled = true;
    }

    public void SendPoints(int points)
    {
        _totalPoints += points;
        UIManager.Instance.UpdateScore(_totalPoints);
    }

    public void SendEnemyCount(int count)
    {
        _totalEnemiesDestroyed += count;
        UIManager.Instance.UpdateEnemyCount(_totalEnemiesDestroyed);
    }
}
