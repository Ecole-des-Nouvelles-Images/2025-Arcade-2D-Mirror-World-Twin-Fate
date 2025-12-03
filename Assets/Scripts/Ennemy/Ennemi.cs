using UnityEngine;

namespace Ennemy
{
    public class Ennemi : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _bulletForce = 20f;
        [SerializeField] private float _damage;
        [SerializeField] private float _cooldown = 2f;
        //public ParticleSystem destroy;

        private float timer = 0f;

        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= _cooldown)
            {
                timer = 0f;
                Shoot();
                //OnDestroy();
            }
        }

        void Shoot()
        {
            GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.down * _bulletForce, ForceMode2D.Impulse);
        }
        // private void OnDestroy()
        // {
        //     if (destroy != null)
        //     {
        //         ParticleSystem clone = Instantiate(
        //             destroy,
        //             transform.position,
        //             destroy.transform.rotation
        //         );
        //         clone.Play();
        //         Destroy(clone.gameObject, 3);
        //     }
        // }
    }
}