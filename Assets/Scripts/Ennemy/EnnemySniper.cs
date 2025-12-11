using UnityEngine;
using System.Collections;

namespace Ennemies
{
    public class EnnemySniper : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _bulletForce = 1f;
        [SerializeField] private float _cooldown = 2f;
        [SerializeField] private float _yCheckAttack = 13f;

        [Header("Cone Shooting")]
        [SerializeField] private int _bulletCount = 5;
        [SerializeField] private float _spreadAngle = 20f;

        [Header("Sniper Aim")]
        [SerializeField] private float sniperDuration = 1.2f;
        [SerializeField] private float blinkSpeed = 10f;

        [SerializeField] private LineRenderer laser;
        private float timer = 0f;
        private Transform player;

        private void Awake()
        {
            GameObject p = GameObject.FindGameObjectWithTag("PlayerRed");
            if (p != null)
                player = p.transform;
            
            laser.enabled = false;
        }

        void Update()
        {
            if (transform.position.y > _yCheckAttack) return;

            timer += Time.deltaTime;
            if (timer >= _cooldown)
            {
                timer = 0f;
                StartCoroutine(SniperSequence());
            }
        }

        IEnumerator SniperSequence()
        {
            if (player == null) yield break;

            laser.material = new Material(laser.material);
            laser.enabled = true;

            float elapsed = 0f;
            Vector2 lastTargetPos = player.position;

            while (elapsed < sniperDuration)
            {
                elapsed += Time.deltaTime;

                lastTargetPos = player.position;

                laser.SetPosition(0, transform.position);
                laser.SetPosition(1, lastTargetPos);

                Material mat = laser.material;
                Color c = mat.color;
                c.a = Mathf.Lerp(0.4f, 1f, Mathf.Abs(Mathf.Sin(Time.time * blinkSpeed)));
                mat.color = c;

                yield return null;
            }

            laser.enabled = false;
            ShootConeAtPlayer(lastTargetPos);
        }


        void ShootConeAtPlayer(Vector2 targetPos)
        {
            Vector2 baseDir = (targetPos - (Vector2)transform.position).normalized;
            float baseAngle = Mathf.Atan2(baseDir.y, baseDir.x) * Mathf.Rad2Deg;

            for (int i = 0; i < _bulletCount; i++)
            {
                float angleOffset = Mathf.Lerp(-_spreadAngle / 2f, _spreadAngle / 2f, i / (float)(_bulletCount - 1));
                float finalAngle = baseAngle + angleOffset;

                Vector2 dir = new Vector2(
                    Mathf.Cos(finalAngle * Mathf.Deg2Rad),
                    Mathf.Sin(finalAngle * Mathf.Deg2Rad)
                );

                GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(dir * _bulletForce, ForceMode2D.Impulse);
            }
        }

        public void SetUpData(EnemyData data)
        {
            _bulletForce = data.bulletForce;
        }
    }
}
