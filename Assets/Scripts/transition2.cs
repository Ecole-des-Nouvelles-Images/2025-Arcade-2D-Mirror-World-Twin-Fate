using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class transition2 : MonoBehaviour
{
    
    

    [SerializeField] private float speed;
    [SerializeField] private float _posx;
    [SerializeField] private float _posy;
    
void Start()
        {
            transform.DOMoveY(_posy, speed);
        }
}
