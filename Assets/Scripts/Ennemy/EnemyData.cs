using UnityEngine;

namespace Ennemies
{
    public enum EnemyColor
    {
        Blue,
        Red
    }

    [CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [Header("Stats")] 
        public int maxHealth;
        public int damage;
        public int ContactDamage;
        public float bulletSpeed;
        public float Cooldown;
    }
}