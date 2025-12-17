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
        
        private float _bulletForce;
        private int _damage;
        private float _yCheckAttack = 13f;
        private float timer = 0f;
        float nextShootTime;
        //bool canShoot = false;
        
        private void Start()
        {
            float randomDelay = Random.Range(0f, 2f);
            nextShootTime = Time.deltaTime + randomDelay;
            //canShoot  = true;
        }
        void Update()
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



        void Shoot()
        {
            GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.down * _bulletForce, ForceMode2D.Impulse);
            SoundFXManager.Instance.PlaySoundFXClip(_attack, SoundGroups.Sfx);
           // _bulletPrefab.GetComponent<EnemyData>().damage = _damage;
        }

        public void SetUpData(EnemyData data)
        {
            _bulletForce = data.bulletForce;
            _damage = data.damage;
           
        }
    }
}