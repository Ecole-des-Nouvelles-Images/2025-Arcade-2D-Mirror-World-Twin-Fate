using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundFXManager : MonoBehaviour
{
    
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public void PlaysoundFXClip(AudioClip audioClip, Transform spawnTranform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTranform.position, Quaternion.identity);
        
        audioSource.clip = audioClip;
        
        audioSource.volume = volume;
        
        audioSource.Play();
        
        float clipLenght = audioSource.clip.length;
        
        Destroy(audioSource.gameObject, clipLenght);
    }

}
