using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{
    //Health
    [SerializeField] private int _health = 10;

    //Shield Cooldown
    [SerializeField] private float _cooldownTimer = 0f;
    [SerializeField] private float _cooldownDuration = 30f;



    //Shield Variables
    private MeshRenderer _meshRenderer;
    private SphereCollider _collider;
    //private float _globalAlpha = 0.33f;

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

        if (_health <= 0)
        {
            Debug.Log("Shield has been destroyed");
            _meshRenderer.enabled = false;
            _collider.enabled = false;
            _isCoolingDown = true;
        }

        if (_isCoolingDown == true)
        {
            Debug.Log("Shield is cooling down");
            _cooldownTimer += Time.deltaTime;
            Debug.Log("TimerIsRunning");
            if (_cooldownTimer >= _cooldownDuration)
            {
                Debug.Log("TimerHasFinished");
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
        Debug.Log("Shield Health has gone down by 1 health");
    }


}
