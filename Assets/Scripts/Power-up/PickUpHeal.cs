using System;
using UnityEngine;

public class PickUpHeal : MonoBehaviour
{
    public HealthBuff powerupEffect;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        powerupEffect.Apply();
    }
}
