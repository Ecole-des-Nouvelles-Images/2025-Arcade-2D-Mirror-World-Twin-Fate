using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
public class CustomButtonScale : MonoBehaviour
{
    private void Start()
    {
        transform.LeanScale(Vector2.zero, 0.2f);
    }

    public void Open()
    {
        transform.LeanScale(Vector2.one, 0.2f);
    }

    public void Close()
    {
        transform.LeanScale(Vector2.zero, 1f).setEaseInBack();
    }
}
