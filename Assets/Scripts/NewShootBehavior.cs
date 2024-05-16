using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class NewShootBehavior : MonoBehaviour
{

    private Ray _ray;
    private RaycastHit _hitInfo;

    public LayerMask _hitLayer;
    void Start()
    {
        _ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));

            if (Physics.Raycast(_ray, out _hitInfo, Mathf.Infinity, _hitLayer))
            {
                EnemyAI enemy = _hitInfo.transform.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    enemy.Damage();
                    Debug.Log("Enemy Took Damage");
                }

                Debug.Log("Hit: " + _hitInfo.transform.name);

                //HeavyEnemyAI heavyEnemy = _hitInfo.transform.GetComponent<HeavyEnemyAI>();
                //if (heavyEnemy != null)
                //{
                //    heavyEnemy.Damage();
                //    Debug.Log("Heavy Enemy took a hit");
                //}
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_ray.origin, _hitInfo.point);
  
    }
}
