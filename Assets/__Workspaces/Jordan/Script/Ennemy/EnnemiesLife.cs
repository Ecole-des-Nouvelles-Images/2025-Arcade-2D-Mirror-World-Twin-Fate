using System;
using __Workspaces.Jordan.Script.Player;
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
        [SerializeField] private int _maxHealth = 3;
        [SerializeField] private ColorType enemyColor; // Choisir Red ou Blue dans l’inspecteur
        public int currentHealth;
        
        private Collider _collider;

        public void TakeDamage(int damage)
        {
            Debug.Log("I'm taken damage " + damage);
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            currentHealth =  _maxHealth;
        } 
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("ca collisionne");
            
                //  Ennemi 
                Debug.Log(" La Couleur de l'ennemi est " +enemyColor + "Le Tag de la collision est "+ collision.gameObject.tag);
                if (enemyColor == ColorType.Blue && collision.collider.CompareTag("BulletBlue"))
                {
                    BulletScript bullet = collision.collider.GetComponent<BulletScript>();
                    TakeDamage(bullet.Damage);
                }

                // Ennemi Rouge
                if (enemyColor == ColorType.Red && collision.collider.CompareTag("BulletRed"))
                {
                    BulletScript bullet = collision.collider.GetComponent<BulletScript>();
                    TakeDamage(bullet.Damage);
                }
        }
    }
}