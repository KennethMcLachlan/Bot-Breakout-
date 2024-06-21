using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class NewShootBehavior : MonoBehaviour
{
    //Spark On Hit
    [SerializeField] private List<GameObject> _sparkHitPool;
    [SerializeField] private GameObject _sparkPrefab;
    [SerializeField] private GameObject _sparkHitContainer;

    //Grenade Launcher Hit
    [SerializeField] private List<GameObject> _grenadeHitPool;
    [SerializeField] private GameObject _grenadeContainer;
    [SerializeField] private GameObject _grenadePrefab;

    private float _despawnRate = 3.0f;

    [SerializeField] private AudioSource _gunShotSFX;

    [SerializeField] private GameObject _muzzleFlashContainer;
    private ParticleSystem _muzzleFlash;

    private Ray _ray;
    private RaycastHit _hitInfo;
    public LayerMask _hitLayer;

    //Fire Gun Variables
    [SerializeField] private float _fireRate = 0.5f;
    private float _canFire = -1f;

    //Rapid Fire Cooldown
    [SerializeField] private float _rapidFireDuration = 5f;

    //PowerUp Variables
    [SerializeField] private bool _fullAutoIsActive;
    [SerializeField] private bool _grenadeLauncherIsActive;

    void Start()
    {
        _ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));

        _gunShotSFX = GetComponent<AudioSource>();
        if (_gunShotSFX == null)
        {
            Debug.Log("Gunshot audio is NULL");
        }

        GenerateSparkHitPool(10);
        GenerateGrenadeHitPool(5);

        _muzzleFlash = _muzzleFlashContainer.GetComponentInChildren<ParticleSystem>();
        if (_muzzleFlash == null)
        {
            Debug.Log("Muzzle Flash is NULL");
        }
    }

    void Update()
    {
        if (_grenadeLauncherIsActive == true)
        {
            _fireRate = 0.5f;
            if (Input.GetMouseButton(0) && Time.time > _canFire)
            {
                FireGrenade();
            }
        }
        else if (_fullAutoIsActive == true)
        {
            _fireRate = 0.1f;
            if (Input.GetMouseButton(0) && Time.time > _canFire) //Rapid Fire
            {
                FireWeapon();
            }
        }
        else
        {
            _fireRate = 0.6f;
            if (Input.GetMouseButtonDown(0) && Time.time > _canFire) //Regular Fire
            {
                FireWeapon();
            }
        }
    }

    private void FireWeapon()
    {
        _canFire = Time.time + _fireRate;
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

            //Shield
            ShieldBehavior shieldBehavior = _hitInfo.transform.GetComponent<ShieldBehavior>();
            if (shieldBehavior != null)
            {
                shieldBehavior.TakeHit();
                Debug.Log("Hit called on the Shield's TakeHit()");
            }

            //PowerUps

            RapidFirePowerup rapidFirePowerup = _hitInfo.transform.GetComponent<RapidFirePowerup>();
            if (rapidFirePowerup != null)
            {
                rapidFirePowerup.TakeHit();
            }

            GrenadePowerup grenadePowerup = _hitInfo.transform.GetComponent<GrenadePowerup>();
            if (grenadePowerup != null)
            {
                grenadePowerup.TakeHit();
            }
            

            Debug.Log("Hit: " + _hitInfo.transform.name);
        }
    }

    private void FireGrenade()
    {
        _canFire = Time.time + _fireRate;
        _gunShotSFX.Play(); //Get Grenade Launcher SFX
        _muzzleFlash.Play(); //Maybe new muzzle flash??

        _ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));

        if (Physics.Raycast(_ray, out _hitInfo, Mathf.Infinity, _hitLayer))
        {
            RequestGrenade();

            //Enemy
            EnemyAI enemy = _hitInfo.transform.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.Damage();
                Debug.Log("Enemy Took Damage");
            }

            //Shield
            ShieldBehavior shieldBehavior = _hitInfo.transform.GetComponent<ShieldBehavior>();
            if (shieldBehavior != null)
            {
                shieldBehavior.TakeHit();
                Debug.Log("Hit called on the Shield's TakeHit()");
            }

            //PowerUps

            RapidFirePowerup rapidFirePowerup = _hitInfo.transform.GetComponent<RapidFirePowerup>();
            if (rapidFirePowerup != null)
            {
                rapidFirePowerup.TakeHit();
            }

            GrenadePowerup grenadePowerup = _hitInfo.transform.GetComponent<GrenadePowerup>();
            if (grenadePowerup != null)
            {
                grenadePowerup.TakeHit();
            }

            Debug.Log("Hit: " + _hitInfo.transform.name);
        }
    }

    public void RecieveRapidFire()
    {
        _fullAutoIsActive = true;
        StartCoroutine(RapidFireActivityRoutine());
    }
    private IEnumerator RapidFireActivityRoutine()
    {
        if (_fullAutoIsActive == true)
        {
            yield return new WaitForSeconds(_rapidFireDuration); // Currently set to 5 seconds
            _fullAutoIsActive = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_ray.origin, _hitInfo.point);
    }

    //Spark Pool
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

    //GrenadePool
    private void GenerateGrenadeHitPool(int amountOfGrenades)
    {
        _grenadeHitPool = new List<GameObject>();

        for (int i = 0; i < amountOfGrenades; i++)
        {
            GameObject grenade = Instantiate(_grenadePrefab);
            grenade.SetActive(false);
            grenade.transform.parent = _grenadeContainer.transform;
            _grenadeHitPool.Add(grenade);
        }
    }

    public GameObject RequestGrenade()
    {
        foreach (var grenade in _grenadeHitPool)
        {
            if (grenade.activeInHierarchy == false)
            {
                ActivateGrenade(grenade);
                return grenade;
            }
        }

        GameObject newGrenade = Instantiate(_grenadePrefab);
        newGrenade.transform.parent = _grenadeContainer.transform;
        _grenadeHitPool.Add(newGrenade);
        ActivateGrenade(newGrenade);

        return newGrenade;
    }

    private void ActivateGrenade(GameObject grenade)
    {
        grenade.transform.position = _hitInfo.point;
        grenade.transform.rotation = Quaternion.identity;
        grenade.SetActive(true);

        GrenadeExplosion(grenade);
        //StartCoroutine(DeactivateGrenadeAfterDelay(grenade, _despawnRate));
    }

    private void GrenadeExplosion(GameObject grenade)
    {
        Collider[] hitColliders = Physics.OverlapSphere(grenade.transform.position, grenade.GetComponent<Collider>().bounds.extents.magnitude);
        foreach(var  hitCollider in hitColliders)
        {
            EnemyAI enemy = hitCollider.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.Damage();
                Debug.Log("Enemy took damage from grenade explosion");
            }
        }

        StartCoroutine(DeactivateGrenadeAfterDelay(grenade, _despawnRate));
    }

    private IEnumerator DeactivateGrenadeAfterDelay(GameObject grenade, float delay)
    {
        yield return new WaitForSeconds(delay);
        grenade.SetActive(false);
    }

    public void RecieveGrenadeLauncher()
    {
        _grenadeLauncherIsActive = true;
        StartCoroutine(GrenadeLauncherCooldownRoutine());
    }

    private IEnumerator GrenadeLauncherCooldownRoutine()
    {
        if (_grenadeLauncherIsActive == true)
        {
            yield return new WaitForSeconds(_rapidFireDuration);
            _grenadeLauncherIsActive = false;
        }
    }

}
