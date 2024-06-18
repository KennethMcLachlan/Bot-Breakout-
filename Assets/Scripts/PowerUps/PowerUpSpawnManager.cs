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
    [SerializeField] private List<Transform> _powerupSpawnPoints;
    [SerializeField] private List<GameObject> _powerupPool;

    private int _powerupID;

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void RandomizePowerupIcon()
    {
        switch (_powerupID)
        {
            case 0:
                //Rapid Fire
                //Random Reposition
                //SetActive
                break;
            case 1:
                //GrenadeLauncher
                //Random Reposition
                //SetActive
                break;
            case 2:
                //Nuke
                //Random Reposition
                //SetActive
                break;
            case 3:
                //Shield remover
                //Random Reposition
                //SetActive
                break;
        }
    }

    //private List<Transform> GetPowerupSpawnPoints()
    //{
    //    //Does this need to be a list?
    //}
    
    private void RepositionPowerup()
    {

    }

    
}
