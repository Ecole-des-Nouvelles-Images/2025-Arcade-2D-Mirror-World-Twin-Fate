using UnityEngine;

namespace __Workspaces.Jordan.Script.Ennemy
{
    public class Ennemi : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletForce = 20f;
        [SerializeField] private float damage;
        [SerializeField] private float cooldown = 2f;

        private float timer = 0f;

        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= cooldown)
            {
                timer = 0f;
                Shoot();
            }
        }

        void Shoot()
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
        }
    }
}