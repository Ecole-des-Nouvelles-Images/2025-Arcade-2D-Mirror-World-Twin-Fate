using System;
using UnityEngine;

public class Bulletdestroyer : MonoBehaviour
{
    public ParticleSystem destroy;
    
    private void OnDestroy()
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
