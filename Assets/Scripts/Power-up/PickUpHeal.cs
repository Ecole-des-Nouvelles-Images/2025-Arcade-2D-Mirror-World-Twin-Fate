using System;
using UnityEngine;

public class PickUpHeal : MonoBehaviour
{
    public ParticleSystem destroy;
    public HealthBuff powerupEffect;
    
    
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerRed") || collision.gameObject.CompareTag("PlayerBlue"))
        {
            DoVFX();
            Destroy(gameObject);
            powerupEffect.Apply();
        }
    }

    private void DoVFX()
    {
        if (destroy != null)
        {
            ParticleSystem clone = Instantiate(
                destroy,
                transform.position,
                destroy.transform.rotation
            );
            clone.Play();
            Destroy(clone.gameObject, 3);
        } 
    }
}
