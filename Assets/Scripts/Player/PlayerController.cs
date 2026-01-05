using __Workspaces.Baptiste.scripts;
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
        [SerializeField] public ParticleSystem _IsChargingEffect;
        [SerializeField] private ParticleSystem _IsChargedEffect;
        [SerializeField] private ControllerRumble _rumble;
        [SerializeField] private AudioClip _attack;
        
        private float _horizontal;
        private float _vertical;
        private bool vfxPlaying = false;
        private bool vfxChargedPlaying = false;
        private bool isFullyCharged = false;
        private Rigidbody2D _rb;
        private Vector2 Move;
        private Animator animator;
        private float timer = 0f;
        private float chargeThreshold = 0.4f;
        private float chargeTime;

        [Header("Inputs")]
        [SerializeField] private bool Firing;
        [SerializeField] private bool IsCharging = false;
        [SerializeField] private float shootCooldown = 0.3f;
        [SerializeField] private float _minChargeTime = 1.5f;
        [SerializeField] private int _chargeMultiplier;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            _rb.linearVelocity = new Vector2(Move.x * _speed, Move.y * _speed);
            animator.SetFloat("xVelocity", _rb.linearVelocity.x);
          
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
            if (timer >= shootCooldown)
            {
                GameObject instantiate = Instantiate(_prfBullet, transform.position, Quaternion.identity);
                instantiate.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _bulletspeed);
                timer = 0f;
                
                if (_attack != null)
                {
                    SoundFXManager.Instance.PlaySoundFXClip(_attack, SoundGroups.Sfx);
                }
            }
        }

        private void StartFire()
        {
            // Début de la charge quand on appuie
            IsCharging = true;
            isFullyCharged = false;
            chargeTime = 0f;
        }

        private void ManageCharging()
        {
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
                DoFire();
            }
            else
            {
                GameObject _bullet = Instantiate(_prfChargedBullet, transform.position, Quaternion.identity);
                _bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _chargedbulletspeed);
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