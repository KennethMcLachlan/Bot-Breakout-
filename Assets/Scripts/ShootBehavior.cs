using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootBehavior : MonoBehaviour
{
    public int damage = 1;
    public float range = 100f;

    
    public Ray _raycastOrigin;
    public RaycastHit _hit;

    public LayerMask _hitLayer;
    private void Start()
    {
        _raycastOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    }
    private void Update()
    {
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity/*, _hitLayer*/))
            {
                Debug.Log("Hit" + hitInfo.collider.gameObject.name);

                EnemyAI enemy = hitInfo.transform.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    Debug.Log("Enemy Hit");
                    enemy.Damage();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * Mathf.Infinity;
        Gizmos.DrawRay(_raycastOrigin);
    }

}
