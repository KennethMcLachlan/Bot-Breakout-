using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishingWall : MonoBehaviour
{
    [SerializeField] private GameObject _wall;
    [SerializeField] private int _wallHealth;
    //public EnemyAI _enemyAI;

    //[SerializeField] private Slider _healthbar;
    private void Start()
    {
        //_wallHealth = 100;
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

            EnemyAI enemyAI = other.GetComponent<EnemyAI>();
            enemyAI.SelfDestruct(); //Destroy enemy when they hit the finishing wall
            Debug.Log("Enemy has collided with the wall");

            _wallHealth -= 10;
            UIManager.Instance.UpdateWallHealth(_wallHealth);
        }

        if (other.tag == "Heavy_Enemy")
        {
            HeavyEnemyAI heavyEnemyAI = other.GetComponent<HeavyEnemyAI>();
            heavyEnemyAI.SelfDestruct();
            Debug.Log("Heavy Enemy has collided with the wall");

            _wallHealth -= 25;
            UIManager.Instance.UpdateWallHealth(_wallHealth);
        }
    }

    //public void WallHeath(int points)
    //{
    //    _wallHealth -= points;

    //    UIManager.Instance.UpdateWallHealth(_wallHealth);
    //}
}
