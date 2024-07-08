using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePowerup : MonoBehaviour
{
    //This script tells the NewShootBehavior to RecieveGrenadeLauncher
    private NewShootBehavior _newShootBehavior;
    private void Start()
    {
        _newShootBehavior = GameObject.Find("Player").GetComponent<NewShootBehavior>();
        if (_newShootBehavior == null)
        {
            Debug.LogError("New Shoot Behavior script is null from the GrenadeLauncher Powerup");
        }
    }

    public void TakeHit()
    {
        _newShootBehavior.RecieveGrenadeLauncher();
        gameObject.SetActive(false);
    }
}
