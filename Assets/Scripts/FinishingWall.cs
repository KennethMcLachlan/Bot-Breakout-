using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishingWall : MonoBehaviour
{
    [SerializeField] private GameObject _wall;
    [SerializeField] private int _wallHealth = 10;
    public EnemyAI _enemyAI;
    private void Start()
    {
        _enemyAI = GetComponent<EnemyAI>();
        if(_enemyAI == null)
        {
            Debug.Log("Enemy AI is NULL!");
        }
    }
    private void Update()
    {
        if (_wallHealth <= 0)
        {
            //Initiate Game Over Sequence
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _wallHealth--;

            _enemyAI.Damage(); //Destroy enemy when they hit the finishing wall

        }
    }
}
