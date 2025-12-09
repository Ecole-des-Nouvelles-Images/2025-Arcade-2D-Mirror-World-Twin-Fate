using Ennemy;
using UnityEngine;

namespace __Workspaces.Jordan.Script
{
    public class Enemy : MonoBehaviour
    {
        [Header("Config S.O")]
        public EnemyData data;

        [SerializeField] private EnemyAttack _enemyAttack;
    
        private int currentHealth;

        private void Awake()
        {
            LoadStats();
        }
        private void LoadStats()
        {
            currentHealth = data.maxHealth;
            _enemyAttack.SetUpData(data);
        }


    }
}