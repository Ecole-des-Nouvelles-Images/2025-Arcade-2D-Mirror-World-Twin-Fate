using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace __Workspaces.Jordan.Script.Ennemy
{
    public enum ColorType
    {
        Red,
        Blue
    }

    public class Ennemy : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 3;
        [SerializeField] private ColorType enemyColor; // Choisir Red ou Blue dans l’inspecteur
        public int currentHealth;
        
        private Collider _collider;

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            currentHealth =  maxHealth;
        } 
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("ca collisionne");
                //  Ennemi Bleu
                if (enemyColor == ColorType.Blue && collision.collider.CompareTag("BulletBlue"))
                {
                    TakeDamage(1);
                    Destroy(gameObject);
                }

                // Ennemi Rouge
                if (enemyColor == ColorType.Red && collision.collider.CompareTag("BulletRed"))
                {
                    TakeDamage(1);
                    Destroy(gameObject);
                }
        }
    }
}