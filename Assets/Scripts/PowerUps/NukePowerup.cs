using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePowerup : MonoBehaviour
{
    private NewShootBehavior _newShootBehavior;
    private NukeBehavior _nukeBehavior;
    void Start()
    {
        _newShootBehavior = GameObject.Find("Player").GetComponent<NewShootBehavior>();
        if (_newShootBehavior == null)
        {
            Debug.LogError("NewShootBehavior is null on the Nuke Powerup Script");
        }

        _nukeBehavior = GameObject.Find("NukeBehavior_Trigger").GetComponent<NukeBehavior>();
        if (_nukeBehavior == null)
        {
            Debug.LogError("NukeBehavior_Trigger was not found o the Nuke Powerup script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeHit()
    {
        _nukeBehavior.RecievePowerupNotification();
        gameObject.SetActive(false);
    }
}
