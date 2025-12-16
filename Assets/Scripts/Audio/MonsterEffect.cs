using System.Collections.Generic;
using __Workspaces.Baptiste.scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class MonsterEffect : MonoBehaviour
    {
        [Header("Sound Effect")] 
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioClip _attack;
        [SerializeField] private AudioClip _damaged;
        [SerializeField] private AudioClip _death;
        
        private void MonsterDamaged() 
        { 
            if (_damaged == null) return;
            // SoundFXManager.Instance.PlaySoundFXClip(_damaged, );
        }
        
        private void MonsterDeath()
        { 
            if (_death == null) return;
            
            // SoundFXManager.Instance.PlaySoundFXClip(_death, 0.8f);
        }

        private void MonsterAttacked()
        {
            if (_attack == null) return;
            
            // SoundFXManager.Instance.PlaySoundFXClip(_attack, 0.8f);
        }
    }
}
