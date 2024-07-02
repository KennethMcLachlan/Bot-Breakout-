using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameCutsceneController : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;

    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _textToSkip;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _startGameManager;
    [SerializeField] private GameObject _resetAssetPositionManager;
    [SerializeField] private GameObject _pauseMenuManager;
    [SerializeField] private GameObject _slidingDoor;
    [SerializeField] private GameObject _cutSceneDoor;
    [SerializeField] private GameObject _npc;

    private bool _anyKeyIsDisabled;
    private bool _playerCanSkip;

    private void Start()
    {
        _director.GetComponent<PlayableDirector>();
        StartCoroutine(TextSkipPrevention());
    }

    private void Update()
    {
        if (_anyKeyIsDisabled == false && Input.anyKey)
        {
            _textToSkip.SetActive(true);
            StartCoroutine(HoldToSetBoolRoutine());
            _anyKeyIsDisabled = true;
        }

        if (_playerCanSkip == true && Input.GetKeyDown(KeyCode.Return))
        {
            SkipCutScene();
        }
    }

    private void SkipCutScene()
    {
        _director.Stop();
        _player.SetActive(true);
        _textToSkip.SetActive(false);
        _canvas.SetActive(true);
        _startGameManager.SetActive(true);
        _resetAssetPositionManager.SetActive(true);
        _pauseMenuManager.SetActive(true);
        _slidingDoor.SetActive(true);
        _cutSceneDoor.SetActive(false);
        _npc.transform.position = new Vector3(0f, 9.9f, 60f);

    }

    private IEnumerator HoldToSetBoolRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _playerCanSkip = true;
    }

    private IEnumerator TextSkipPrevention()
    {
        yield return new WaitForSeconds(35f);
        _anyKeyIsDisabled = true;
        _textToSkip.SetActive(false);
    }

}
