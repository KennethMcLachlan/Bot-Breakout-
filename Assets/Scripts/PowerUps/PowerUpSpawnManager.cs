using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{
    private static PowerUpSpawnManager _instance;

    public static PowerUpSpawnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("PowerupSpawnManager is NULL");
            }
            return _instance;
        }
    }

    //Lists
    [SerializeField] private Transform[] _powerupSpawnPoints;
    [SerializeField] private List<GameObject> _powerupPool;

    private NewShootBehavior _newShootBehavior;

    private int _powerupID;

    private bool _waveIsActive;

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        _newShootBehavior = GameObject.Find("Player").GetComponent<NewShootBehavior>();
        if (_newShootBehavior == null)
        {
            Debug.LogError("Powerup Manager has not accessed the Player Script");
        }
    }
    
    private void RandomizePowerupIcon()
    {
        GameObject powerupToReposition = null;

        switch (_powerupID)
        {
            case 0:
                //Rapid Fire
                //_newShootBehavior.RecieveRapidFire();
                //Random Reposition
                powerupToReposition = _powerupPool[0];
                break;
            case 1:
                //GrenadeLauncher
                //Random Reposition
                powerupToReposition = _powerupPool[1];
                break;
            case 2:
                //Nuke
                //Random Reposition
                powerupToReposition = _powerupPool[2];
                break;
            case 3:
                //Shield remover
                //Random Reposition
                powerupToReposition = _powerupPool[3];
                break;
            default:
                Debug.LogError("There is no Powerup");
                break;
        }

        if (powerupToReposition != null)
        {
            RepositionPowerup(powerupToReposition);
            powerupToReposition.SetActive(true);
        }

    }

    private IEnumerator SpawnPowerupRoutine()
    {
        //randomize which powerup ID will be called on

        while (_waveIsActive == true)
        {
            int randomPowerupID = Random.Range(0, 3);
            float randomPowerUpSpawnTime = Random.Range(1f, 5f); //Adjust time. Was at 15f, 45f

            yield return new WaitForSeconds(randomPowerUpSpawnTime);
            _powerupID = randomPowerupID;
            RandomizePowerupIcon();
        }
    }
    
    private void RepositionPowerup(GameObject powerup)
    {
        int randomSpawnPoint = Random.Range(0, _powerupSpawnPoints.Length);
        powerup.transform.position = _powerupSpawnPoints[randomSpawnPoint].position;
    }

    //Recieves Notification that the Wave is active
    public void WavesAreActive()
    {
        _waveIsActive = true;
        StartCoroutine(SpawnPowerupRoutine());
    }

    //Recieves Notification that Waves are inactive
    public void WavesAreInactive()
    {
        _waveIsActive = false;
    }
}
