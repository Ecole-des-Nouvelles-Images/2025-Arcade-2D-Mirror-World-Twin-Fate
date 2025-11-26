using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomButtonPosition : MonoBehaviour
{
    public void StartJumping()
    {
        transform.LeanMoveLocal(new Vector2(0, 0), 0.2f);
    }

    public void GoBack()
    {
        transform.LeanMoveLocal(new Vector2(-1350, 0), 0.2f);
    }
}
