using __Workspaces.Baptiste.scripts;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Player
{
    public class SharedPlayersLife : MonoBehaviourSingleton<SharedPlayersLife>
    {
        [Header("Settings")]
        public float _maxHealth = 10f;
        public float _currentHealth;
        [SerializeField] private AudioClip _damaged;
        
        [Header("References")]
        public Image _healthBar;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _healthBar.fillAmount = _currentHealth / _maxHealth;
            
            if (_damaged)
            {
                SoundFXManager.Instance.PlaySoundFXClip(_damaged, SoundGroups.Sfx);
            }
        }

        public void IncreaseHealth(int amount)
        {
            _currentHealth ++;
            _healthBar.fillAmount = _currentHealth / _maxHealth;
        }
        
        public bool IsDead()
        {
            if (_currentHealth <= 0) return true;
            else return false;
        }
        
    }
}