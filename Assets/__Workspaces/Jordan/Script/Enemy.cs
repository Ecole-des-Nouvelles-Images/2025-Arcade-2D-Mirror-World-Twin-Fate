using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Config S.O")]
    public EnemyData data;

    private int currentHealth;

    private void Start()
    {
        LoadStats();
    }

    private void LoadStats()
    {
        currentHealth = data.maxHealth;

        // // permet d'instancier le modèle depuis le SO
        // if (data.prefab != null)
        //     Instantiate(data.prefab, transform);
    }


}