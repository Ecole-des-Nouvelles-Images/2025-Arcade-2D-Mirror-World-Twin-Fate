using System;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    public Test powerupEffect;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        powerupEffect.Apply();
    }
}
