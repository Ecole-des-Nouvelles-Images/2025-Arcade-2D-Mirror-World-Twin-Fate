using System;
using Ennemies;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    public float bulletLife = 1f;
    public float speed = 5f;
    private float timer;
    [SerializeField] private ParticleSystem destroy;
    
    private BulletSpawner _bulletSpawner;
    
    public int SetDamage(int damage) => _damage = damage;
    public int GetDamage() => _damage;
    public void SetUpBullet(BulletSpawner bulletSpawner) {
        _bulletSpawner = bulletSpawner;
        _bulletSpawner.DestroyBullet += BulletSpawnerOnDestroyBullet;
    }

    private void OnDestroy() {
        if(_bulletSpawner==null) return;
        _bulletSpawner.DestroyBullet -= BulletSpawnerOnDestroyBullet;
    }

    private void BulletSpawnerOnDestroyBullet() =>Destroy(gameObject);

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        bulletLife -= Time.deltaTime;
        if (bulletLife <= 0f) Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBlue") && gameObject.CompareTag("BulletEnemy"))
        {
            DoVFX();
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("PlayerRed") && gameObject.CompareTag("BulletEnemy"))
        {
            DoVFX();
            Destroy(gameObject);
        }
    }
    private void DoVFX()
    {
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