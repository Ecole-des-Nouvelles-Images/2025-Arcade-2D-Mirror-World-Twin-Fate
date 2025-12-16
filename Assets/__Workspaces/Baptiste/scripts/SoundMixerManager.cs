using UnityEngine;
using UnityEngine.Audio;

namespace __Workspaces.Baptiste.scripts
{
    public class SoundMixerManager : MonoBehaviour
    {
        [Header("Sound Settings")]
        [SerializeField] private AudioMixer audioMixer;

        public void SetMasterVolume(float level)
        {
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(level) * 20f);
        }

        public void SetSFXSoundVolume(float level)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(level) * 20f);
        }

        public void SetMusicVolume(float level)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
        }
    
        public void SetAmbianceVolume(float level)
        {
            audioMixer.SetFloat("AmbianceVolume", Mathf.Log10(level) * 20f);
        }
    }
}
