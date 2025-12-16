using __Workspaces.Baptiste.scripts;
using UnityEngine;
using UnityEngine.Audio;

namespace Player
{
    public class PlayerEffect : MonoBehaviour
    {
        [Header("Sound Effect")]
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioClip _attack;
        [SerializeField] private AudioClip _damaged;
        [SerializeField] private AudioClip _death;
        
        private void PlayerDamaged() 
        { 
            if (_damaged == null) return;
          
            // SoundFXManager.Instance.PlaySoundFXClip(_damaged, 0.8f);
        }
        
        private void PlayerDeath()
        { 
            if (_death == null) return;
            
            // SoundFXManager.Instance.PlaySoundFXClip(_death, 0.8f);
        }

        private void PlayerAttacked()
        {
            if (_attack == null) return;
            
            // SoundFXManager.Instance.PlaySoundFXClip(_attack, 0.8f);
        }
    }
}

