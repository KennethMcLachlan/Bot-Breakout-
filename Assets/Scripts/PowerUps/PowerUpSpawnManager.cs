using System.Collections;
using System.Collections.Generic;
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

    private int _powerupID;

    private bool _waveIsActive;

    private void Awake()
    {
        _instance = this;
    }
    
    private void RandomizePowerupIcon()
    {
        GameObject powerupToReposition = null;

        switch (_powerupID)
        {
            case 0:
                //Rapid Fire
                powerupToReposition = _powerupPool[0];
                break;
            case 1:
                //Rapid Fire
                powerupToReposition = _powerupPool[0];
                break;
            case 2:
                //Rapid Fire
                powerupToReposition = _powerupPool[0];
                break;
            case 3:
                //Rapid Fire
                powerupToReposition = _powerupPool[0];
                break;
            case 4:
                //GrenadeLauncher
                powerupToReposition = _powerupPool[1];
                break;
            case 5:
                //GrenadeLauncher
                powerupToReposition = _powerupPool[1];
                break;
            case 6:
                //GrenadeLauncher
                powerupToReposition = _powerupPool[1];
                break;
            case 7:
                //Shield remover
                powerupToReposition = _powerupPool[2];
                break;
            case 8:
                //Shield remover
                powerupToReposition = _powerupPool[2];
                break;
            case 9:
                //Nuke
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
            StartCoroutine(DeactivatePowerupRoutine(powerupToReposition));
        }

    }

    private IEnumerator SpawnPowerupRoutine()
    {
        //Randomize which Powerup ID will be called on

        while (_waveIsActive == true)
        {
            int randomPowerupID = Random.Range(0, 10);
            float randomPowerUpSpawnTime = Random.Range(15f, 45f);

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

    //Unclaimed Powerup will be set to "inactive" after duration
    private IEnumerator DeactivatePowerupRoutine(GameObject powerup)
    {
        yield return new WaitForSeconds(10f);
        powerup.SetActive(false);
    }

    //Receives Notification that the Wave is active
    public void WavesAreActive()
    {
        _waveIsActive = true;
        StartCoroutine(SpawnPowerupRoutine());
    }

    //Receives Notification that Waves are inactive
    public void WavesAreInactive()
    {
        _waveIsActive = false;
    }
}
