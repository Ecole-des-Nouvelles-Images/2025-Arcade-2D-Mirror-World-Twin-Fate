using System;
using Ennemies;
using UnityEngine;

namespace Player
{
    public class PlayerBulletScript : MonoBehaviour
    {
        [SerializeField] public int _damage;
        private Rigidbody2D rb;
        [SerializeField] private float _delayToDestroy;
        public ParticleSystem destroy;
        public ParticleSystem hitdestroy;
        public enum ProjectileColor { Red, Blue }
        public bool isCharged = false;  // vrai si c'est un tir chargé
        public ProjectileColor color;
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

            //Debug.Log("il subit des dégats");

            if (collision.gameObject.CompareTag("EnnemieRed") || collision.gameObject.CompareTag("EnnemieBlue"))
            {
                DestroyBullet();
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("EnnemieRed"))
            {
                if (color == ProjectileColor.Red)
                {
                    // Tir chargé : fait des dégâts, mais ne se détruit pas
                    collision.GetComponent<EnemyLife>().TakeDamage(Damage);
                    DoHitVFX();
                    if (!isCharged)
                        DestroyBullet();
                }
                else
                {
                    // Tir opposé : détruit le projectile
                    DestroyBullet();
                }
            }
            else if (collision.CompareTag("EnnemieBlue"))
            {
                if (color == ProjectileColor.Blue)
                {
                    collision.GetComponent<EnemyLife>().TakeDamage(Damage);
                    DoHitVFX();
                    if (!isCharged) 
                        DestroyBullet();
                }
                else
                {
                    DestroyBullet();
                }
            }
        }

        private void DoHitVFX() {
            if (hitdestroy != null)
            {
                ParticleSystem clone = Instantiate(
                    hitdestroy,
                    transform.position,
                    destroy.transform.rotation
                );
                clone.Play();
                Destroy(clone.gameObject, 3);
            }
        }
        private void DoDestroyVFX() {
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

        private void DestroyBullet()
        {
            DoDestroyVFX();
            Destroy(gameObject);
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

  

