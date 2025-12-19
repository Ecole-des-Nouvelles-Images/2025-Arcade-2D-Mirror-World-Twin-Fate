using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class transition : MonoBehaviour
{
    [Header("Parallax Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float _posx;
    [SerializeField] private float _posy;
    [SerializeField] private string _nameScene;
    
    void Start()
    {
        transform.DOMoveY(_posy, speed).OnComplete(Transition);
    }

    private void Transition()
    {
        SceneManager.LoadScene(_nameScene);
    }
}
