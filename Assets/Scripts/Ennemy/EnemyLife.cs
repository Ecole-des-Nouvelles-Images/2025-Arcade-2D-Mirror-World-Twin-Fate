using __Workspaces.Baptiste.scripts;
using Player;
using UnityEngine;

namespace Ennemies
{
    public enum ColorType
    {
        Red,
        Blue
    }
    public class EnemyLife : MonoBehaviour
    {
        [Header("settings")]
        [SerializeField] private ColorType enemyColor; // Choisir Red ou Blue dans l’inspecteur
        [SerializeField] private EnemyData _data;
        [SerializeField] private AudioClip _damaged;
        [SerializeField] private AudioClip _death;
        
        private float _flashTime = 0.3f;
        private float currentIntensity = 0f;
        private float _timerIntensity;
        private int currentHealth;
        private AnimationCurve _flashAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        private Material mat;
        private Collider _collider;
        public ParticleSystem _deathParticles;
        
           private void Start()
        {
            if (_data != null)
            {
                currentHealth = _data.maxHealth;
            }
            else
            {
                Debug.Log("No enemies found");
                currentHealth = 1;
            }
            mat = GetComponent<SpriteRenderer>().material;
            currentHealth = _data.maxHealth;
        }
        
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
            GetComponent<EnemyHitFeedback>().PlayHitAnimation();
            Debug.Log("I'm taken damage " + damage);
            currentHealth -= damage;
            _timerIntensity = _flashTime;
            if (currentHealth <= 0)
            {
                DoDeathVFX();
                //SoundFXManager.Instance.PlaySoundFXClip(_damaged,  SoundGroups.Sfx);
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
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