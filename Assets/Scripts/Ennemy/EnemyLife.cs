using Player;
using UnityEngine;

namespace Ennemy
{
    public enum ColorType
    {
        Red,
        Blue
    }

    public class EnemyLife : MonoBehaviour
    {
        [Header("settings")] [SerializeField] private int _maxHealth = 3;
        [SerializeField] private ColorType enemyColor; // Choisir Red ou Blue dans l’inspecteur

        private float _flashTime = 0.3f;
        private float currentIntensity = 0f;
        private float _timerIntensity;
        public int currentHealth;
        private AnimationCurve _flashAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        public Material mat;
        private Collider _collider;
        public ParticleSystem _deathParticles;

        private void Update()
        {
            if (_timerIntensity > 0f)
            {
                _timerIntensity -= Time.deltaTime;
                float t = _timerIntensity / _flashTime;
                mat.SetFloat("_Hit_intensity", _flashAnimationCurve.Evaluate(t));

                if (_timerIntensity <= 0f)
                {
                    _timerIntensity = 0;
                    mat.SetFloat("_Hit_intensity", 0);
                }
            }
        }

        public void TakeDamage(int damage)
        {
            Debug.Log("I'm taken damage " + damage);
            currentHealth -= damage;
            _timerIntensity = _flashTime;
            if (currentHealth <= 0)
            {
                DoDeathVFX();
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            mat = GetComponent<SpriteRenderer>().material;
            currentHealth = _maxHealth;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("ca collisionne");
            //  Ennemi 
            if (enemyColor == ColorType.Blue && collision.collider.CompareTag("BulletBlue"))
            {
                PlayerBulletScript bullet = collision.collider.GetComponent<PlayerBulletScript>();
                TakeDamage(bullet.Damage);
            }

            if (enemyColor == ColorType.Red && collision.collider.CompareTag("BulletRed"))
            {
                PlayerBulletScript bullet = collision.collider.GetComponent<PlayerBulletScript>();
                TakeDamage(bullet.Damage);
            }
        }
        private void OnDestroy()
        {
            
        }

        private void DoDeathVFX() {
            if (_deathParticles != null)
            {
                ParticleSystem clone = Instantiate(
                    _deathParticles,
                    transform.position,
                    _deathParticles.transform.rotation
                );
                clone.Play();
                Destroy(clone.gameObject, 3);
            }
        }
    }
}