using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFadeIn : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 3f;
    private CanvasGroup _canvasGroup;
    private float _targetAlpha = 1f;
    private float _fadeSpeed;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        if (_canvasGroup == null)
        {
            Debug.LogError("Canvas Group was not found at Start of Fade");
        }

        _fadeSpeed = _targetAlpha / _fadeDuration;
    }

    private void Update()
    {
        if (_canvasGroup.alpha < _targetAlpha)
        {
            _canvasGroup.alpha += _fadeSpeed * Time.deltaTime;
        }
        
    }
}
