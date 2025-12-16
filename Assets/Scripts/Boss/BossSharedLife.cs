using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Boss
{
    public class BossSharedLife : MonoBehaviourSingleton<BossSharedLife>

    {
        [Header("Settings")] 
        [SerializeField] private float _maxHealth = 10f;
        [SerializeField] private float _currentHealth;

        [Header("References")] [SerializeField]
        private Image _healthBar;

        public bool IsAlive {
            get => _currentHealth > 0;
        }

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _healthBar.fillAmount = _currentHealth / _maxHealth;
        }

        public bool IsDead()
        {
            if (_currentHealth <= 0) return true;
            else return false;
        }
    }
}