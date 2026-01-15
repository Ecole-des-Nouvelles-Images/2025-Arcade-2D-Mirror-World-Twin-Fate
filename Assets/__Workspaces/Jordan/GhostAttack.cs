using Ennemies;
using UnityEngine;

public class GhostAttack : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private EnemyData _data;
    private int Damage;

    private void OntriggerEnter2D()
    {
        Damage = _data.damage;
    }
}

