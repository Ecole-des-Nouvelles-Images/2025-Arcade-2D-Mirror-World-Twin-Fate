using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;

namespace Power_up
{
    public abstract class PowerUp : ScriptableObject
    { 
        public abstract void Execute(GameObject player);
    }

    [CreateAssetMenu(fileName = "HealUp", menuName = "PowerUp/Healing")]
    public class HealUp : PowerUp
    {
        [Header("Regain de vie")]
        [SerializeField] private float healAmount = 1;

        public override void Execute(GameObject player)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            SharedPlayersLife Health = playerHealth.GetSharedPlayersLife();
            player.GetComponent<SharedPlayersLife>()._maxHealth += healAmount;
            if (Health != null)
            {
                //Health.IncreaseHealth(_currentHealth);
            }
        }
    }
}