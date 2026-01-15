using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class custom_UI_scale : MonoBehaviour
{

    [SerializeField] private float _scale;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _curve =  AnimationCurve.EaseInOut(0,0,1,1);

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _delayBeforTransition = 5;
    [SerializeField] private float _rotatex;
    [SerializeField] private float _rotatey;
    [SerializeField] private float _rotatez;
    

    private bool _doTimer;
    private float _timer;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.DOScale(_scale, _duration).SetEase(_curve);
        _canvasGroup.DOFade(1, _delayBeforTransition).OnComplete(DoTransition);
        transform.DORotate(new Vector3(0, 0, _rotatez), _duration).SetEase(_curve);
    }

    private void DoTransition()
    {
        
    }

    public void PlayTimer()
    {
        _doTimer = true;
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_doTimer)
        {
            _timer += Time.deltaTime;
            if (_timer >= _delayBeforTransition)
            {
                float t = _timer / _delayBeforTransition;
                _canvasGroup.alpha = t;
                //fait ma transtion
                _doTimer = false;
            }
        }
    }
}
