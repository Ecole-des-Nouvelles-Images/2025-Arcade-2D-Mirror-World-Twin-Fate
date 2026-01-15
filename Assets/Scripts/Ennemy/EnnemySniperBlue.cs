using UnityEngine;
using System.Collections;

namespace Ennemies
{
    public class EnnemySniperBlue : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _bulletForce = 1f;
        [SerializeField] private float _cooldown = 2f;

        [Header("Sniper Aim")]
        [SerializeField] private float sniperDuration = 1.2f;

        [SerializeField] private LineRenderer laser;

        private float timer = 0f;
        private Transform player;

        private void Awake()
        {
            GameObject p = GameObject.FindGameObjectWithTag("PlayerBlue");
            if (p != null)
                player = p.transform;

            laser.enabled = false;
        }

        void Update()
        {
            if (player == null) return;

            timer += Time.deltaTime;
            if (timer >= _cooldown)
            {
                timer = 0f;
                StartCoroutine(SniperSequence());
            }
        }

        IEnumerator SniperSequence()
        {
            laser.enabled = true;

            float elapsed = 0f;
            Vector2 targetPos = player.position;

            while (elapsed < sniperDuration)
            {
                elapsed += Time.deltaTime;

                // Le laser suit la position du joueur
                targetPos = player.position;

                laser.SetPosition(0, transform.position);
                laser.SetPosition(1, targetPos);

                yield return null;
            }

            laser.enabled = false;

            ShootAtPlayer(targetPos);
        }

        void ShootAtPlayer(Vector2 targetPos)
        {
            Vector2 dir = (targetPos - (Vector2)transform.position).normalized;

            GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            rb.AddForce(dir * _bulletForce, ForceMode2D.Impulse);
        }

        public void SetUpData(EnemyData data)
        {
            _bulletForce = data.bulletSpeed;
        }
    }
}
