using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using __Workspaces.Baptiste.scripts;
using NUnit.Framework;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _flashTime = 0.3f;
        [SerializeField] private AnimationCurve _flashAnimationCurve = AnimationCurve.EaseInOut(0,0,1,1);
        [SerializeField] private ControllerRumble _rumble;
        [SerializeField] private ParticleSystem _deathVFX;
        [SerializeField] private float deathDelay = 1.5f;
        [SerializeField] private GameObject TransitionPrefab;
        [SerializeField] private GameObject DeathUIPrefab;
        [SerializeField] private AudioClip _death;
        [SerializeField] private PlayerController _player;
        
        private Collider2D _collider;
        private Material _mat;
        private float currentIntensity = 0f;
        private float _timerIntensity;
        private SpriteRenderer _spriteRenderer;
        private bool IsDead = true;
        
        private void Start()
        {
            IsDead = false;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _mat = GetComponent<SpriteRenderer>().material;
            _collider = GetComponent<Collider2D>();
        }
        private void Update()
        {
            if (_timerIntensity > 0f) {
                _timerIntensity -= Time.deltaTime;
                float t = _timerIntensity/_flashTime;
                _mat.SetFloat("_Hit_intensity", _flashAnimationCurve.Evaluate(t));

                if (_timerIntensity <= 0f) {
                    _timerIntensity = 0;
                    _mat.SetFloat("_Hit_intensity", 0);
                }
            }
            if (SharedPlayersLife.Instance.IsDead() && !IsDead)
            {
                Die();
            }
        }
        public void DoFeedback()
        {
            _rumble.Rumble(0.01f, 0.0f, 0.15f);
            _timerIntensity = _flashTime;
        }
        public void Die()
        {
            if (!IsDead)
            {
                SoundFXManager.Instance.PlaySoundFXClip(_death, SoundGroups.Sfx);
                _rumble.StopRumble();
                _player.enabled = false;
                _spriteRenderer.enabled = false;
                DoDeathVFX();
                StartCoroutine(DeathRoutine());
                IsDead = true;
            }
        }
        
        private IEnumerator DeathRoutine()
        {
            yield return new WaitForSeconds(deathDelay);
            DeathUIPrefab.SetActive(true);
            yield return new WaitForSeconds(deathDelay);
            TransitionPrefab.SetActive(true);
            // SceneManager.LoadScene("GameOver");
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("BulletEnemy"))
            {
                DoFeedback();
                SharedPlayersLife.Instance.TakeDamage(1);
            }
            if (other.CompareTag("BulletEnnemieBlue") &&  gameObject.CompareTag("PlayerBlue"))
            {
                DoFeedback();
                SharedPlayersLife.Instance.TakeDamage(1);
            }
            if (other.CompareTag("BulletEnnemieRed") &&  gameObject.CompareTag("PlayerRed"))
            {
                DoFeedback();
                SharedPlayersLife.Instance.TakeDamage(1);
            }
            if (other.CompareTag("EnnemieBlue") || other.CompareTag("EnnemieRed"))
            {
                DoFeedback();
                SharedPlayersLife.Instance.TakeDamage(1);
            }
        }

        private void DoDeathVFX()
        {
            if (_deathVFX != null)
            {
                ParticleSystem clone = Instantiate(
                    _deathVFX,
                    transform.position,
                    _deathVFX.transform.rotation
                );
                clone.Play();
                Destroy(clone.gameObject, 3);
            }
        }
    }
}