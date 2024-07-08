using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParentToBooth : MonoBehaviour
{

    [SerializeField] private Transform _player;

    void Start()
    {
        _player.transform.parent = gameObject.transform;
    }

}
