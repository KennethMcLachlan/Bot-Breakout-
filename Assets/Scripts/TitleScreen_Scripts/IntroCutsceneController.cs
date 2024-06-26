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

    private bool _anyKeyIsEnabled;
    private bool _playerCanSkip;

    void Start()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }
    void Update()
    {
        if (_anyKeyIsEnabled == false && Input.anyKey)
        {
            //_skipTextIsActive = true;
            _textToSkip.SetActive(true);
            StartCoroutine(HoldToSetBoolRoutine());
            _anyKeyIsEnabled = true;

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
}


