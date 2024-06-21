using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFirePowerup : MonoBehaviour
{
    //This script tells the new shoot behavior to RecieveRapidFire
    private NewShootBehavior _newShootBehavior;
    private void Start()
    {
        _newShootBehavior = GameObject.Find("Player").GetComponent<NewShootBehavior>();
        if (_newShootBehavior == null)
        {
            Debug.LogError("New Shoot Behavior script is null from the Rapid Fire Powerup");
        }
    }

    public void TakeHit()
    {
        _newShootBehavior.RecieveRapidFire();
        //add sfx
        //add pfx
        //add wait for seconds before setting the object to false
        gameObject.SetActive(false);
    }
}
