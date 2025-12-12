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
        private float _horizontal;
        private float _vertical;

        [Header("Inputs")] [SerializeField]
        public bool Firing;
        private Rigidbody2D _rb;
        public bool IsCharging = false;
        public float chargeTime;
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
            IsCharging = true;
            chargeTime = 0f;
        }

        private void ManageCharging()
        {
            Debug.Log("ca Charge");
            chargeTime += Time.deltaTime;
        }

        private void Cancel()
        {
            // Quand on relâche, on lance l'attaque chargée si la charge est assez grande
            if (chargeTime <= _minChargeTime)
            {
                Debug.Log("ca envoie");
                DoFire();
            }
            else
            {
                GameObject _bullet = Instantiate(_prfChargedBullet, transform.position, Quaternion.identity);
                _bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _chargedbulletspeed);

                // dégâts multiplier
                Debug.Log("ca envoie x2");
                _bullet.GetComponent<PlayerBulletScript>().Damage = _damage * _chargeMultiplier;
            }
            chargeTime = 0f;
            IsCharging = false;
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