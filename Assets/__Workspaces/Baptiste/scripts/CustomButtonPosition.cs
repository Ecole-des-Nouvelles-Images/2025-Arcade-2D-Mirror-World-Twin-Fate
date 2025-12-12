using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
public class CustomButtonPosition : MonoBehaviour
{
    public GameObject firstselectedbutton;
    public GameObject returnselectedbutton;
    public void StartJumping()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstselectedbutton);
        transform.LeanMoveLocal(new Vector2(0, 0), 0.2f);
    }
    
    public void GoBack()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(returnselectedbutton);
        transform.LeanMoveLocal(new Vector2(-1720, 0), 0.2f);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        transform.LeanMoveLocal(new Vector2(0, 0), 0.2f);
        Time.timeScale = 0f;
    }
}