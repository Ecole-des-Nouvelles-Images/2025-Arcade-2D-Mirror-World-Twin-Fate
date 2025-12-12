using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class transition : MonoBehaviour
{

    [SerializeField] private float speed;
    //[SerializeField] private float size;
    //[SerializeField] private AnimationCurve _curve ;
    [SerializeField] private float _posx;
    [SerializeField] private float _posy;
    void Start()
    {
        transform.DOMoveY(_posy, speed);
    }
    
}
