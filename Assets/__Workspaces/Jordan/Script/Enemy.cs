using Ennemy;
using UnityEngine;

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

        // // permet d'instancier le modèle depuis le SO
        // if (data.prefab != null)
        //     Instantiate(data.prefab, transform);
    }


}