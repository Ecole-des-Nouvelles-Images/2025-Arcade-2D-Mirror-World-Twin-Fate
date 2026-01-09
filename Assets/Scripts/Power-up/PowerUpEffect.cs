using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;

namespace Power_up
{
    public abstract class PowerUp : ScriptableObject
    {
        public int powerUpID;
        public abstract void Execute(GameObject player);
    }

    [CreateAssetMenu(fileName = "ShootSpeedUp", menuName = "PowerUp/ShootSpeeding")]
    public class ShootSpeedUp : PowerUp
    {
        [Header("Augmentation de la cadence de tir")]
        [SerializeField] private float fireRateMultiplier = 0.8f;

        public override void Execute(GameObject player)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
               // playerController.shootCooldown *= fireRateMultiplier;
            }
        }
    }

    [CreateAssetMenu(fileName = "DamageUp", menuName = "PowerUp/Damaging")]
    public class DamageUp : PowerUp
    {
        [Header("Multiplicateur des dégats")]
        [SerializeField] private int damageMultiplier = 1;

        public override void Execute(GameObject player)
        {
            PlayerBulletScript playerShoot = player.GetComponent<PlayerBulletScript>();
            if (playerShoot != null)
            {
                playerShoot._damage *= damageMultiplier;
            }
        }
    }

    [CreateAssetMenu(fileName = "HealUp", menuName = "PowerUp/Healing")]
    public class Healing : PowerUp
    {
        [Header("Regain de vie")]
        [SerializeField] private float healAmount = 1;

        public override void Execute(GameObject player)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            SharedPlayersLife Health = playerHealth.GetSharedPlayersLife();
            if (Health != null)
            {
                //Health.IncreaseHealth(_currentHealth);
            }
        }
    }
}