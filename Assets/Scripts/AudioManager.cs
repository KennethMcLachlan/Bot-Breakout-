using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Audio Manager is NULL");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

    }

    [SerializeField] private AudioSource _enemyExplosion;

    private void Start()
    {
        if (_enemyExplosion == null)
        {
            _enemyExplosion = GetComponent<AudioSource>();
            if (_enemyExplosion == null)
            {
                Debug.LogError("AudioSource component is missing on this GameObject.");
            }
        }
    }

    public void EnemyExplosionSFX()
    {

        _enemyExplosion.Play();

    }
}
