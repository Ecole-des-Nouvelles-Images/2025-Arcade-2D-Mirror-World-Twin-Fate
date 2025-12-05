using UnityEngine;

namespace Ennemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject _bulletPrefab;
        private float _bulletForce = 20f;
        private float _damage;
        private float _cooldown = 2f;
        private float _yCheckAttack = 10f;
        private EnemyData _data;

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
        }

        public void SetUpData(EnemyData data)
        {
            _bulletForce   = data.bulletForce;
            _damage = data.damage;
            
        }
    }
}