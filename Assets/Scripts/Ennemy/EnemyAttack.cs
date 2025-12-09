using UnityEngine;
using System.Collections;

namespace Ennemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject _bulletPrefab;
        private float _bulletForce = 20f;
        private float _damage;
        private float _cooldown = 2f;
        private float _yCheckAttack = 13f;

        private float timer = 0f;

        [Header("Sniper Settings")]
        [SerializeField] private bool _useSniper = false;
        [SerializeField] private float _sniperCooldown = 4f;
        [SerializeField] private float _aimDuration = 1.5f;
        [SerializeField] private float _laserWidth = 0.12f;
        [SerializeField] private float _blinkSpeed = 10f;
        [SerializeField] private float _maxSniperDistance = 50f;

        private float sniperTimer = 0f;
        private bool isAiming = false;
        private LineRenderer laser;

        private Transform player;

        private void Awake()
        {
            GameObject p = GameObject.FindGameObjectWithTag("PlayerBlue");
            if (p != null)
                player = p.transform;
            else
                Debug.LogError("PlayerBlue not found! Check the tag.");

            if (_useSniper)
            {
                laser = gameObject.AddComponent<LineRenderer>();
                laser.positionCount = 2;
                laser.startWidth = _laserWidth;
                laser.endWidth = _laserWidth;
                laser.material = new Material(Shader.Find("Sprites/Default"));
                laser.startColor = Color.red;
                laser.endColor = Color.red;
                laser.enabled = false;

                laser.sortingLayerName = "Foreground";
                laser.sortingOrder = 10;
            }
        }

        private void Update()
        {
            if (transform.position.y > _yCheckAttack) return;

            // Tir normal
            timer += Time.deltaTime;
            if (timer >= _cooldown)
            {
                timer = 0f;
                Shoot();
            }

            // Sniper
            if (_useSniper && player != null)
            {
                sniperTimer += Time.deltaTime;

                if (!isAiming && sniperTimer >= _sniperCooldown)
                {
                    isAiming = true;
                    StartCoroutine(SniperSequence());
                }

                if (isAiming)
                    UpdateSniperLaser();
            }
        }

        private void Shoot()
        {
            if (_bulletPrefab == null) return;

            GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.AddForce(Vector2.down * _bulletForce, ForceMode2D.Impulse);
        }

        private IEnumerator SniperSequence()
        {
            sniperTimer = 0f;
            float aimTimer = 0f;
            laser.enabled = true;

            Vector3 startPos = transform.position;
            Vector3 targetPos = player.position;
            float distance = Vector3.Distance(startPos, targetPos);
            
            while (aimTimer < _aimDuration)
            {
                aimTimer += Time.deltaTime;
                float t = aimTimer / _aimDuration;

                // Extension progressive du laser
                Vector3 currentEnd = Vector3.Lerp(startPos, targetPos, t);

                // Clignotement/fade
                float alpha = Mathf.Abs(Mathf.Sin(Time.time * _blinkSpeed));
                Color c = new Color(1f, 0f, 0f, alpha);

                laser.startColor = c;
                laser.endColor = c;

                laser.SetPosition(0, startPos);
                laser.SetPosition(1, currentEnd);

                yield return null;
            }

            // Tir final
            ShootSniperRay();
            laser.enabled = false;
            isAiming = false;
        }

        private void UpdateSniperLaser()
        {
            if (player == null) return;

            Vector3 startPos = transform.position;
            Vector3 targetPos = player.position;

            laser.SetPosition(0, startPos);
            laser.SetPosition(1, targetPos);
        }

        private void ShootSniperRay()
        {
            if (player == null) return;

            Vector2 dir = (player.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, _maxSniperDistance);

            if (hit.collider != null && hit.collider.CompareTag("PlayerBlue"))
            {
                Debug.Log("Sniper hit player! Damage: " + _damage);
            }
        }

        public void SetUpData(EnemyData data)
        {
            _bulletForce = data.bulletForce;
            _damage = data.damage;
        }
    }
}
