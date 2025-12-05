using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CustomButtonPosition : MonoBehaviour
{
    public void StartJumping()
    {
        transform.LeanMoveLocal(new Vector2(0, 0), 0.2f);
    }
    
    public void GoBack()
    {
        transform.LeanMoveLocal(new Vector2(-1720, 0), 0.2f);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        transform.LeanMoveLocal(new Vector2(0, 0), 0.2f);
        Time.timeScale = 0f;
    }
}