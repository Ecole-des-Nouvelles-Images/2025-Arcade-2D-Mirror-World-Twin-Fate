using UnityEngine;

namespace __Workspaces.Jordan.Script
{
    public enum EnemyColor
    {
        Blue,
        Red
    }

    [CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [Header("Idd")]
        public EnemyColor color;

        [Header("Stats")]
        public int maxHealth = 3;
        public int damage = 1;
        public float bulletForce = 5f;
    }
}