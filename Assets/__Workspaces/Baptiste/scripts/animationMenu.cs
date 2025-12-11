using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class animationMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float posx, posy; 
    [SerializeField] private float _cycleLenght = 2;
    [SerializeField] private float rotate = 45;
    void Start()
    {
        transform.DOMove(new Vector2(posx, posy), _cycleLenght).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
        
        
        //transform.DORotate(new Vector3(0,0,45), _cycleLenght).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
