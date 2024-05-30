using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishingWall : MonoBehaviour
{
    //[SerializeField] private GameObject _wall;
    [SerializeField] private int _wallHealth;

    [SerializeField] private ParticleSystem _explosionAnim;

    [SerializeField] private AudioSource _explosionSFX;

    private void Start()
    {
        _explosionSFX = GetComponent<AudioSource>();
        if (_explosionSFX == null)
        {
            Debug.Log("ExplosionAudio is NULL");
        }
    }
    private void Update()
    {
        if (_wallHealth <= 0)
        {
            Time.timeScale = 0;
            UIManager.Instance.GameOverSequence();
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

            _explosionAnim.Play();
            _explosionSFX.Play();
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
