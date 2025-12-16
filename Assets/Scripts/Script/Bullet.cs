using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 1f;
    public float speed = 5f;

    private float timer;
    public ParticleSystem destroy;
    
    private BulletSpawner _bulletSpawner;

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
        if (other.gameObject.CompareTag("PlayerBlue") && gameObject.CompareTag("BulletEnnemieBlue"))
        {
            //Debug.Log("il subit des dégats");
            DoVFX();
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("PlayerRed") && gameObject.CompareTag("BulletEnnemieRed"))
        {
            //Debug.Log("il subit des dégats");
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