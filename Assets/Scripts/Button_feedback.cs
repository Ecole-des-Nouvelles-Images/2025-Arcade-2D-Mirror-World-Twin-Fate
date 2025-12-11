using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Button_feedback : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{

    [SerializeField] private float _size = 1.2f;
    [SerializeField] private float _speed = 0.3f;
    [SerializeField] private AnimationCurve _curve =  AnimationCurve.EaseInOut(0,0,1,1);
    [SerializeField] private float _rotate = 2;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        transform.DOPause();
        transform.DOScale(_size , _speed ).SetEase(_curve);
        transform.DORotate(new Vector3(0,0,_rotate), _speed);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        transform.DOPause();
        transform.DOScale(1, _speed ).SetEase(_curve);
        transform.DORotate(new Vector3(0,0,0), _speed);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        transform.DOPause();
        transform.DOScale(_size , _speed ).SetEase(_curve);
        transform.DORotate(new Vector3(0,0,_rotate), _speed);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("OnPointerExit");
        transform.DOPause();
        transform.DOScale(1, _speed ).SetEase(_curve);
        transform.DORotate(new Vector3(0,0,0), _speed);
    }
}
