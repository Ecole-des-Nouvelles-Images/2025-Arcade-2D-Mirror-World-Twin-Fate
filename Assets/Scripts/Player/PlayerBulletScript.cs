using System;
using Ennemies;
using UnityEngine;

namespace Player
{
    public enum ProjectileColor { Red, Blue }
    public class PlayerBulletScript : MonoBehaviour
    {
        [Header("Bullet Settings")]
        [SerializeField] private float _delayToDestroy;
        [SerializeField] private ParticleSystem destroy;
        [SerializeField] private ParticleSystem hitdestroy;
        [SerializeField] private ProjectileColor color;
        
        public int _damage;
        private bool isCharged = false;  // vrai si c'est un tir chargé
        private Rigidbody2D rb;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, _delayToDestroy);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("EnnemieRed") || collision.gameObject.CompareTag("EnnemieBlue"))
            {
                //collision.GetComponent<EnemyLife>().TakeDamage(Damage);
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
                    collision.GetComponent<EnemyLife>().TakeDamage(_damage);
                    DoHitVFX();
                    if (!isCharged) DestroyBullet();
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
                    collision.GetComponent<EnemyLife>().TakeDamage(_damage);
                    DoHitVFX();
                    if (!isCharged) DestroyBullet();
                }
                else
                {
                    DestroyBullet();
                }
            }
            else if (collision.CompareTag("BulletDestroyer"))
            {
                {
                    DestroyBulletNoVFX();
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
        private void DestroyBulletNoVFX()
        {
            Destroy(gameObject);
        }
    }
}