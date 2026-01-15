using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class transition : MonoBehaviour
{
    [SerializeField] private float speed;
    [Space(5)]
    [SerializeField] private Vector2 _startAnchorMin= new Vector2(0,0);
    [SerializeField] private Vector2 _startAnchorMax= new Vector2(1,0);
    [Space(5)]
    [SerializeField] private Vector2 _endAnchorMin=new Vector2(0,3);
    [SerializeField] private Vector2 _endAnchorMax=new Vector2(1,3);
    [SerializeField] private string _nameScene;
    private float _timer;
    private float _transitionDuration = 2;
    private bool _isTransitioning = false;
    
    void Start()
    {
        RectTransform rect = transform.GetComponent<RectTransform>();
        rect.anchorMin = _startAnchorMin;
        rect.anchorMax = _startAnchorMax;
        rect.DOAnchorMin(_endAnchorMin, speed);
        rect.DOAnchorMax(_endAnchorMax, speed);
    }

    private void Update()
    {
        if (_isTransitioning = true)
        {
            _timer += Time.deltaTime;
            if (_timer >= _transitionDuration)
            {
                SceneManager.LoadScene(_nameScene);
            }
        }
       
    }
}
