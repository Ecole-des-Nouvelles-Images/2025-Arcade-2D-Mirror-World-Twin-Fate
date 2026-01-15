using UnityEngine;
using System.Collections;

namespace Ennemies
{
    public class EnnemySniper : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float cooldown = 2f;

        [Header("Sniper Aim")]
        [SerializeField] private float aimDuration = 1.2f;
        [SerializeField] private LineRenderer laser;

        private Transform player;
        private float timer;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("PlayerRed")?.transform;
            laser.enabled = false;
        }
        private void Update()
        {
            if (!player) return;

            timer += Time.deltaTime;
            if (timer < cooldown) return;

            timer = 0f;
            StartCoroutine(SniperSequence());
        }
        private IEnumerator SniperSequence()
        {
            laser.enabled = true;

            float time = 0f;
            Vector2 targetPos = player.position;

            while (time < aimDuration)
            {
                time += Time.deltaTime;
                targetPos = player.position;

                laser.SetPosition(0, transform.position);
                laser.SetPosition(1, targetPos);

                yield return null;
            }

            laser.enabled = false;
            Shoot(targetPos);
        }

        private void Shoot(Vector2 targetPos)
        {
            Vector2 direction = (targetPos - (Vector2)transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>(); // à compléter si nécessaire
        }
    }
}