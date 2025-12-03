using System;
using __Workspaces.Jordan.Script.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace __Workspaces.Jordan.Script.Ennemy
{
    public enum ColorType
    {
        Red,
        Blue
    }

    public class Ennemy : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 3;
        [SerializeField] private ColorType enemyColor; 
        public int currentHealth;
        public Material mat;       
        [SerializeField] private float _flashTime = 0.3f;
        [SerializeField] private AnimationCurve _flashAnimationCurve = AnimationCurve.EaseInOut(0,0,1,1);
        private float currentIntensity = 0f;
        private float _timerIntensity;

        private Collider _collider;

        private void Update() {
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

        public void TakeDamage(int damage) {
            currentHealth -= damage;
            _timerIntensity = _flashTime;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            mat = GetComponent<SpriteRenderer>().material;
            currentHealth =  _maxHealth;
        } 
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (enemyColor == ColorType.Blue && collision.collider.CompareTag("BulletBlue"))
            {
                BulletScript bullet = collision.collider.GetComponent<BulletScript>();
                TakeDamage(bullet.Damage);
                //Debug.Log("ca collisionne bleu");
            }
            if (enemyColor == ColorType.Red && collision.collider.CompareTag("BulletRed"))
            {
                RedBulletdestroyer bullet = collision.collider.GetComponent<RedBulletdestroyer>();
                TakeDamage(bullet.Damage);
                //Debug.Log("ca collisionne rouge");
            }
        }
    }
}