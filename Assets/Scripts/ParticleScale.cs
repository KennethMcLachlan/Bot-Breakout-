using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScale : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _scaleMultiplier = 1f;

    private void Start()
    {
        if (_particleSystem == null)
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        UpdateScale();
    }

    private void Update()
    {
        UpdateScale();
    }

    private void UpdateScale()
    {
        var mainModule = _particleSystem.main;
        mainModule.startSizeMultiplier = transform.localScale.x * _scaleMultiplier;
    }
}
