using __Workspaces.Baptiste.scripts;
using Ennemies;
using UnityEngine;

namespace Script
{
    public class EnemyAttack : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _cooldown = 2f;
        [SerializeField] private AudioClip _attack;
        
        private float _bulletForce;
        private int _damage;
        private float _yCheckAttack = 13f;
        private float timer = 0f;

        void Update()
        {
            if( transform.position.y >_yCheckAttack)return;
            timer += Time.deltaTime;
            if (timer >= _cooldown)
            {
                timer = 0f;
                Shoot();
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