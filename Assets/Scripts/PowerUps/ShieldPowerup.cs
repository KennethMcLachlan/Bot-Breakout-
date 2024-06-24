using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : MonoBehaviour
{
    //This script tells the NewShootBehavior to RecieveShieldDisabler
    private ShieldPowerupBehavior _shieldPowerupBehavior;
    private void Start()
    {
        _shieldPowerupBehavior = GameObject.Find("ShieldBehavior_Manager").GetComponent<ShieldPowerupBehavior>();
        if (_shieldPowerupBehavior == null)
        {
            Debug.Log("ShieldPowerupBehavior is NULL");
        }
    }

    public void TakeHit()
    {
        _shieldPowerupBehavior.RecieveShieldPowerupNotifictaion();
        gameObject.SetActive(false);
    }
}
