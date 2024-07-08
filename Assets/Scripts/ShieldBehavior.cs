using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{
    //Health
    [SerializeField] private int _health = 10;

    //Shield Cooldown
    [SerializeField] private float _cooldownTimer = 0f;
    [SerializeField] private float _cooldownDuration = 30f;

    [SerializeField] private AudioSource _shieldDown;
    [SerializeField] private AudioSource _shieldUp;

    //Shield Variables
    private MeshRenderer _meshRenderer;
    private SphereCollider _collider;

    private bool _isCoolingDown;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        if (_health > 5)
        {
            _meshRenderer.material.color = Color.cyan;
            var alpha = _meshRenderer.material.color;
            alpha.a = 0.3f;
            _meshRenderer.material.color = alpha;
        }
        else
        {
            _meshRenderer.material.color = Color.red;
            var alpha = _meshRenderer.material.color;
            alpha.a = 0.3f;
            _meshRenderer.material.color = alpha;
        }

        if (_health <= 0 && !_isCoolingDown)
        {
            _shieldDown.Play();
            _meshRenderer.enabled = false;
            _collider.enabled = false;
            _isCoolingDown = true;
        }

        if (_isCoolingDown == true)
        {
            Debug.Log("Shield is cooling down");
            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer >= _cooldownDuration)
            {
                _shieldUp.Play();
                _meshRenderer.enabled = true;
                _collider.enabled = true;
                _cooldownTimer = 0f;
                Debug.Log("Shield has recharged");
                _health = 10;
                _isCoolingDown = false;
            }
        }
    }

    public void TakeHit()
    {
        _health--;
    }
}
