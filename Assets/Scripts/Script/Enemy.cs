using Ennemies;
using Script;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Config S.O")] 
    [SerializeField] private EnemyData data;
    [SerializeField] private EnemyAttack _enemyAttack;
    private int _currentHealth;

    private void Awake() 
    { 
        LoadStats();
    }

    public int GetContactDamage() => data.ContactDamage;
    public int GetDamage() => data.damage;
    
    private void LoadStats() 
    { 
        _currentHealth = data.maxHealth; 
      if (_enemyAttack != null)  _enemyAttack.SetUpData(data);
    }
}