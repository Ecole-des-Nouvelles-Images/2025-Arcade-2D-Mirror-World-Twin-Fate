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
        [SerializeField] private int _damage = 2;
        [SerializeField] public ParticleSystem _IsChargingEffect;
        [SerializeField] private ParticleSystem _IsChargedEffect;
        [SerializeField] ControllerRumble _rumble;
        private float _horizontal;
        private float _vertical;
        private bool vfxPlaying = false;
        private bool vfxChargedPlaying = false;
        private bool isFullyCharged = false;

        [Header("Inputs")] [SerializeField]
        public bool Firing;
        private Rigidbody2D _rb;
        public bool IsCharging = false;
        public float chargeTime;
        public float chargeThreshold = 0.4f;
        public float _minChargeTime = 1.5f;
        public int _chargeMultiplier;
        public Vector2 Move;
        private Animator animator;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            animator.SetFloat("xVelocity", _rb.linearVelocity.x);
            _rb.linearVelocity = new Vector2(Move.x * _speed, Move.y * _speed);
          
            if (IsCharging)
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
        }

        private void StartFire()
        {
            // Début de la charge quand on appuie
            //_IsChargingEffect.Play(true);
            IsCharging = true;
            isFullyCharged = false;
            chargeTime = 0f;
        }

        private void ManageCharging()
        {
            //Debug.Log("ca Charge");
            //_IsChargingEffect.Play(true);
            chargeTime += Time.deltaTime;
            if (chargeTime >= chargeThreshold && !vfxPlaying)
            {
                _IsChargingEffect.Play();
                _rumble.Rumble(0f, 0.01f, 0f);
                vfxPlaying = true;
            }
            if (chargeTime >= _minChargeTime && !vfxChargedPlaying)
            {
                _rumble.Rumble(0f, 1f, 0.1f);
                _IsChargedEffect.Play();
                _IsChargingEffect.Stop();
                vfxChargedPlaying = true;
            }
        }

        private void Cancel()
        {
            // Quand on relâche, on lance l'attaque chargée si la charge est assez grande
            if (chargeTime <= _minChargeTime)
            {
                //Debug.Log("ca envoie");
                DoFire();
            }
            else
            {
                GameObject _bullet = Instantiate(_prfChargedBullet, transform.position, Quaternion.identity);
                _bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _chargedbulletspeed);
                
                Debug.Log("ca envoie x2");
                _bullet.GetComponent<PlayerBulletScript>().Damage = _damage * _chargeMultiplier;
            }
            _rumble.StopRumble();
            chargeTime = 0f;
            IsCharging = false;
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