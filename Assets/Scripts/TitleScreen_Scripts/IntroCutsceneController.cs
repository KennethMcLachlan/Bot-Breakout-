using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroCutsceneController : MonoBehaviour
{
    private PlayableDirector _playableDirector;

    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _textToSkip;
    [SerializeField] private GameObject _bgMusic;
    [SerializeField] private GameObject _explosionContainer;

    private bool _anyKeyIsDisabled;
    private bool _playerCanSkip;

    void Start()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        StartCoroutine(SkipTextPreventionRoutine());
    }
    void Update()
    {
        if (_anyKeyIsDisabled == false && Input.anyKey)
        {
            //_skipTextIsActive = true;
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
        _playableDirector.Stop();
        _bgMusic.SetActive(true);
        _textToSkip.SetActive(false);
        _canvas.SetActive(true);
        _explosionContainer.SetActive(true);
    }

    private IEnumerator HoldToSetBoolRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _playerCanSkip = true;
    }

    private IEnumerator SkipTextPreventionRoutine()
    {
        yield return new WaitForSeconds(10f);
        _anyKeyIsDisabled = true;
        _textToSkip.SetActive(false);
    }
}


