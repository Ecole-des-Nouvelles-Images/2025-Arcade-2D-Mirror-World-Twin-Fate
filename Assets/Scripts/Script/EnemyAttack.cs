using System;
using System.Collections;
using __Workspaces.Baptiste.scripts;
using Ennemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
    public class EnemyAttack : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _firerate = 2f;
        [SerializeField] private AudioClip _attack;
        [SerializeField] private float _bulletForce;
        [SerializeField] private int _damage;
        
        private float _yCheckAttack = 13f;
        private float timer = 0f;
        private float nextShootTime;
        
        private void Start()
        {
            float randomDelay = Random.Range(0f, 2f);
            nextShootTime = Time.deltaTime + randomDelay;
        }
        private void Update()
        {
            if( transform.position.y >_yCheckAttack)return;
            timer += Time.deltaTime;
            if (timer >= nextShootTime)
            {
                timer = 0f;
                Shoot();
                nextShootTime = Time.deltaTime + _firerate;
            }
        }
        private void Shoot()
        {
            GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.down * _bulletForce, ForceMode2D.Impulse);
            bullet.GetComponent<EnemyBulletScript>().SetDamage(_damage);
            SoundFXManager.Instance.PlaySoundFXClip(_attack, SoundGroups.Sfx);

            if (_attack != null)
            {
                SoundFXManager.Instance.PlaySoundFXClip(_attack, SoundGroups.Sfx);
            }
        }

        public void SetUpData(EnemyData data)
        {
            _bulletForce = data.bulletSpeed;
            _damage = data.damage;
        }
    }
}