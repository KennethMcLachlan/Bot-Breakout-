using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class NewShootBehavior : MonoBehaviour
{
    [SerializeField] private List<GameObject> _sparkHitPool;
    [SerializeField] private GameObject _sparkPrefab;
    [SerializeField] private GameObject _sparkHitContainer;


    private float _despawnRate = 3.0f;

    [SerializeField] private AudioSource _gunShotSFX;

    [SerializeField] private GameObject _muzzleFlashContainer;
    private ParticleSystem _muzzleFlash;

    private Ray _ray;
    private RaycastHit _hitInfo;
    public LayerMask _hitLayer;

    private LayerMask _shieldLayer;
    void Start()
    {
        _ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));

        _gunShotSFX = GetComponent<AudioSource>();
        if (_gunShotSFX == null)
        {
            Debug.Log("Gunshot audio is NULL");
        }

        GenerateSparkHitPool(10);

        _muzzleFlash = _muzzleFlashContainer.GetComponentInChildren<ParticleSystem>();
        if (_muzzleFlash == null)
        {
            Debug.Log("Muzzle Flash is NULL");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _gunShotSFX.Play();
            _muzzleFlash.Play();

            _ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));

            if (Physics.Raycast(_ray, out _hitInfo, Mathf.Infinity, _hitLayer))
            {
                RequestSpark();

                //Enemy
                EnemyAI enemy = _hitInfo.transform.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    enemy.Damage();
                    Debug.Log("Enemy Took Damage");
                }
                else
                {
                    //Shield
                    ShieldBehavior shieldBehavior = _hitInfo.transform.GetComponent<ShieldBehavior>();
                    if (shieldBehavior != null)
                    {
                        shieldBehavior.TakeHit();
                        Debug.Log("Hit called on the Shield's TakeHit()");
                    }
                }

                Debug.Log("Hit: " + _hitInfo.transform.name);
            }

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_ray.origin, _hitInfo.point);
  
    }

    private void GenerateSparkHitPool(int amountOfSparks)
    {
        _sparkHitPool = new List<GameObject>();

        for (int i = 0; i < amountOfSparks; i++)
        {
            GameObject spark = Instantiate(_sparkPrefab);
            spark.SetActive(false);
            spark.transform.parent = _sparkHitContainer.transform;
            _sparkHitPool.Add(spark);
        }

    }

    public GameObject RequestSpark()
    {
        foreach (var spark in _sparkHitPool)
        {
            if (spark.activeInHierarchy == false)
            {
                ActivateSpark(spark);
                return spark;
            }
        }

        GameObject newSpark = Instantiate(_sparkPrefab);
        newSpark.transform.parent = _sparkHitContainer.transform;
        _sparkHitPool.Add(newSpark);
        ActivateSpark(newSpark);

        return newSpark;
    }

    private void ActivateSpark(GameObject spark)
    {
        spark.transform.position = _hitInfo.point;
        spark.transform.rotation = Quaternion.identity;
        spark.SetActive(true);

        StartCoroutine(DeactivateSparkAfterDelay(spark, _despawnRate));
    }

    private IEnumerator DeactivateSparkAfterDelay(GameObject spark, float delay)
    {
        yield return new WaitForSeconds(delay);
        spark.SetActive(false);
    }
    
}
