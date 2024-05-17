using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParentToBooth : MonoBehaviour
{

    [SerializeField] private Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        _player.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
