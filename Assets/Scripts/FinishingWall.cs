using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishingWall : MonoBehaviour
{
    [SerializeField] private GameObject _wall;
    [SerializeField] private int _health = 10;

    private void Update()
    {
        if (_health <= 0)
        {
            //Initiate Game Over Sequence
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _health--;
        }
    }
}
