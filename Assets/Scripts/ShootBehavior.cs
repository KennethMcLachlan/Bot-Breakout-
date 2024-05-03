using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehavior : MonoBehaviour
{
    public int damage = 1;
    public float range = 100f;

    public LayerMask _hitLayer;
    [SerializeField] private GameObject _enemyForDebugging;
    [SerializeField] private Camera _mainCamera;
    private void Start()
    {
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    private void Update()
    {
        if (_mainCamera == null)
        {
            Debug.Log("Main Camera is NULL!");
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray rayOrigin = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, _hitLayer))
            {
                Debug.DrawLine(rayOrigin.origin, hitInfo.point);
                Debug.Log("Hit" + hitInfo.collider.gameObject.name);

                EnemyAI enemy = hitInfo.transform.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    Debug.Log("Enemy Hit");
                    enemy.Damage();

                    //Instantiate(_enemyForDebugging, hitInfo.point, Quaternion.identity);

                    //if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    //{
                    //    Debug.Log("Hit Enemy");
                    //}
                }

                //Shoot();
            }
        }
    }

    //void Shoot()
    //{
    //    RaycastHit hitInfo;
    //    if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hitInfo, range))
    //    {
    //        Debug.Log("Hit: " + hitInfo.transform.name);
    //    }
    //}
}
