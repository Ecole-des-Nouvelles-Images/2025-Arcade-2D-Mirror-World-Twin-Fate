using Ennemies;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private GameObject _prfBullet;
        [SerializeField] private GameObject _prfChargedBullet;
        [SerializeField] private float _speed;
        [SerializeField] private float _bulletspeed;
        [SerializeField] private float _chargedbulletspeed;
        [SerializeField] private int _damage;
        [SerializeField] private ParticleSystem _IsChargingEffect;
        [SerializeField] private ParticleSystem _IsChargedEffect;
       
        private float _horizontal;
        private float _vertical;
        private bool vfxPlaying = false;
        private bool vfxChargedPlaying = false;
        private bool isFullyCharged = false;
        private float _chargeTime;
        private float _chargeThreshold = 0.4f;
        private bool _firing;
        private bool _isCharging = false; 
        private Rigidbody2D _rb;
        private Vector2 Move;
        private Animator animator;

        [Header("Inputs")]
        [SerializeField] private float _minChargeTime = 1.5f;
        [SerializeField] private int _chargeMultiplier; 
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            animator.SetFloat("xVelocity", _rb.linearVelocity.x);
            _rb.linearVelocity = new Vector2(Move.x * _speed, Move.y * _speed);
          
            if (_isCharging)
            {
                ManageCharging();
            }
        }
        
        private void DoLocomotion()
        {
            _rb.linearVelocity = new Vector2(Move.x * _speed, Move.y * _speed);
        }

        private void DoFire()
        {
            GameObject instantiate = Instantiate(_prfBullet, transform.position, Quaternion.identity);
            instantiate.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _bulletspeed);
            
            _prfBullet.GetComponent<PlayerBulletScript>().Damage = _damage;
        }

        private void StartFire()
        {
            // Début de la charge quand on appuie
            _isCharging = true;
            isFullyCharged = false;
            _chargeTime = 0f;
        }

        private void ManageCharging()
        {
            _chargeTime += Time.deltaTime;
            if (_chargeTime >= _chargeThreshold && !vfxPlaying)
            {
                _IsChargingEffect.Play();
                vfxPlaying = true;
            }
            if (_chargeTime >= _minChargeTime && !vfxChargedPlaying)
            {
                _IsChargedEffect.Play();
                _IsChargingEffect.Stop();
                vfxChargedPlaying = true;
            }
        }

        private void Cancel()
        {
            // Quand on relâche, on lance l'attaque chargée si la charge est assez grande
            if (_chargeTime <= _minChargeTime)
            {
                DoFire();
            }
            else
            {
                GameObject _bullet = Instantiate(_prfChargedBullet, transform.position, Quaternion.identity);
                _bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _chargedbulletspeed);
                _bullet.GetComponent<PlayerBulletScript>().Damage = _damage * _chargeMultiplier;
            }
            _chargeTime = 0f;
            _isCharging = false;
            _IsChargingEffect.Stop();
            _IsChargedEffect.Stop();
            vfxPlaying = false;
            isFullyCharged = false;
            vfxChargedPlaying = false;
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                StartFire();
            }

            if (context.canceled)
            {
                Cancel();
            }
        }
    }
}