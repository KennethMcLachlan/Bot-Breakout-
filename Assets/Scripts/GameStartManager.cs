using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] private GameObject _bgMusic;

    private void Start()
    {
        Debug.Log("GameStart Manager just ran Start()");
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        yield return new WaitForSeconds(4f);
        _bgMusic.SetActive(true);
        UIManager.Instance.UpdateWaves();
        Debug.Log("Start Game Routine has run");
    }
}
