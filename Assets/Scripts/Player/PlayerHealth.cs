using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _flashTime = 0.3f;
        [SerializeField] private AnimationCurve _flashAnimationCurve = AnimationCurve.EaseInOut(0,0,1,1);
        [SerializeField] ControllerRumble _rumble;
        private Collider2D _collider;
        public Material mat;
        private float currentIntensity = 0f;
        private float _timerIntensity;
        
        private void Start()
        {
            mat = GetComponent<SpriteRenderer>().material;
            _collider = GetComponent<Collider2D>();
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
        }
        public void DoFeedback()
        {
            _rumble.Rumble(0.01f, 0.0f, 0.15f);
            _timerIntensity = _flashTime;
        }
        public void Die()
        {
            Debug.Log("Mort");
            SceneManager.LoadScene("GameOver");
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("BulletEnemy"))
            {
                DoFeedback();
                SharedPlayersLife.Instance.TakeDamage(1);
                if (SharedPlayersLife.Instance.IsDead())
                {
                    Die();
                }
            }
            if (other.CompareTag("EnnemieBlue") || other.CompareTag("EnnemieRed"))
            {
                DoFeedback();
                SharedPlayersLife.Instance.TakeDamage(1);
                if (SharedPlayersLife.Instance.IsDead())
                {
                    Die();
                }
            }
        }
    }
}