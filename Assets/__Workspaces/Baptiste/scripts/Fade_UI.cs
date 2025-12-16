using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Fade_UI : MonoBehaviour
{
    private bool IsFaded = false;
    
    [SerializeField] private int fadeInAmount = 0;
    [SerializeField] private int fadeOutAmount = 1;
    
    [SerializeField] private float _fadeDuration = 0.2f;
    
    [SerializeField] private CanvasGroup _canvasGroup;

    public void Fader()
    {
        IsFaded = !IsFaded;
        
        if (IsFaded)
        {
            _canvasGroup.DOFade(fadeInAmount, _fadeDuration).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
        }

        else
        {
            _canvasGroup.DOFade(fadeOutAmount, _fadeDuration);
        }
    }
}
