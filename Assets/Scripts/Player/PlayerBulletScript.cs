using System;
using Ennemies;
using UnityEngine;

namespace Player
{
    public class PlayerBulletScript : MonoBehaviour
    {
        [SerializeField] private int _damage;
        private Rigidbody2D rb;
        [SerializeField] private float _delayToDestroy;
        public ParticleSystem destroy;

        public int Damage {
            get => _damage;
            set => _damage = value;
        }
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, _delayToDestroy);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnDestroy();

            //Debug.Log("il subit des dégats");

            if (collision.gameObject.CompareTag("EnnemieRed") || collision.gameObject.CompareTag("EnnemieBlue"))
            {
                DoVFX();
                Destroy(gameObject);

            }
        }
        private void OnDestroy()
        {

        }

        private void DoVFX() {
            if (destroy != null)
            {
                ParticleSystem clone = Instantiate(
                    destroy,
                    transform.position,
                    destroy.transform.rotation
                );
                clone.Play();
                Destroy(clone.gameObject, 3);
            }
        }
    }
}
        
        
        // private void OnCollisionEnter2D(Collision2D collision)
        // {
        //     // If base projectile and bad enemy then destroy
        //     if (collision.gameObject.CompareTag("EnnemieBlue") && gameObject.CompareTag("BulletRed")) DestroyMe();
        //     if (collision.gameObject.CompareTag("EnnemieRed") && gameObject.CompareTag("BulletBlue")) DestroyMe();
        //     
        //     // Damage the enemy
        //     collision.gameObject.GetComponent<EnemyLife>().TakeDamage(Damage);
        //     
        //     // Destroy if the bullet is not mega
        //     if (!_megaBullet) DestroyMe();
        // }
        //
        // private void DestroyMe()
        // {
        //     Destroy(gameObject);
        //     DoVFX();
        // }

        // private void OnTriggerEnter2D(Collider2D other)
        // {
        //     if (other.CompareTag("EnnemieRed") && gameObject.CompareTag("ChargedBulletBlue"))
        //     {
        //         other.GetComponent<EnemyLife>().TakeDamage(Damage);
        //         DoVFX();
        //         Destroy(gameObject);
        //     }
        //     if (other.gameObject.CompareTag("EnnemieBlue") && gameObject.CompareTag("ChargedBulletRed"))
        //     {
        //         other.GetComponent<EnemyLife>().TakeDamage(Damage);
        //         DoVFX();
        //         Destroy(gameObject);
        //         
        //     }
        // }

  

