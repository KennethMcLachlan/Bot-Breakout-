using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCutscenePositions : MonoBehaviour
{
    [SerializeField] private GameObject _npc;
    [SerializeField] private GameObject _slidingDoor;
    void Start()
    {
        ResetAssets();
    }

    private void ResetAssets()
    {
        _npc.transform.position = new Vector3(0f, 9.9f, 60f);
    }
}
