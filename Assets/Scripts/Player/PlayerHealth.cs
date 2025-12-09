using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Settings")]
        public int maxHealth = 10;
        public int currentHealth;
        private Collider2D _collider;public Material mat;       
        [SerializeField] private float _flashTime = 0.3f;
        [SerializeField] private AnimationCurve _flashAnimationCurve = AnimationCurve.EaseInOut(0,0,1,1);
        private float currentIntensity = 0f;
        private float _timerIntensity;
        //public HealthBar healthBar;
        
        private void Start()
        {
            mat = GetComponent<SpriteRenderer>().material;
            _collider = GetComponent<Collider2D>();
            currentHealth = maxHealth;
            // healthBar.SetMaxHealth(maxHealth);
        }
        private void Update()
        {
            if (_timerIntensity > 0f) {
                _timerIntensity -= Time.deltaTime;
                float t = _timerIntensity/_flashTime;
                mat.SetFloat("_Hit_intensity", _flashAnimationCurve.Evaluate(t));

                if (_timerIntensity <= 0f) {
                    _timerIntensity = 0;
                    mat.SetFloat("_Hit_intensity", 0);
                }
            }
            if (StaticData.IsDead())
            {
                Die();
            }
        }
        public void TakeDamage(int damage)
        {
            StaticData.TakeDamage(damage);
            Debug.Log("dégats");
            //currentHealth -= damage;
            _timerIntensity = _flashTime;
            ////healthBar.SetHealth(currentHealth);
            //if (currentHealth <= 0)
            //{
            //    Die(); 
            //}
        }
        public void Die()
        {
            Debug.Log("Mort");
            Destroy(gameObject);
            Invoke("RestartLevel", 5);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("BulletEnemy"))
            {
                TakeDamage(1);
            }
            if (other.CompareTag("EnnemieBlue") || other.CompareTag("EnnemieRed"))
            {
                Debug.Log("Enemycollision");
                TakeDamage(1);
            }
        }
    }
}