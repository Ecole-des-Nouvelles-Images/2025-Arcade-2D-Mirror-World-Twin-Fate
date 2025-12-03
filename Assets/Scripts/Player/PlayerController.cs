using Ennemy;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private GameObject _prfBullet;
        [SerializeField] private float _speed;
        [SerializeField] private float _bulletspeed;
        [SerializeField] private int _damage = 2;
        private float _horizontal;
        private float _vertical;

        [Header("Inputs")] 
        [SerializeField] public bool Firing;
        private Rigidbody2D _rb;
        public bool isCharging = false;
        public float chargeTime;
        public float _minChargeTime;
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
            //DoFire();
           // DoChargedFire();
        }

        private void DoLocomotion()
        {
            // float horizontal = Move.x;
            // float vertical = Move.y;
            // float xMove = horizontal * Time.deltaTime * _speed;
            // float yMove = vertical * Time.deltaTime * _speed;

            _rb.linearVelocity = new Vector2(Move.x * _speed, Move.y * _speed);
        }

        private void DoFire()
        {
           // if (Firing)
            //{
                GameObject instantiate = Instantiate(_prfBullet, transform.position, Quaternion.identity);
                instantiate.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _bulletspeed);
           // }
        }

        private void DoChargedFire()
        {
            {
                // Début de la charge quand on appuie
                if (Firing)
                {
                    isCharging = true;
                    chargeTime = 0f;
                }

                // On augmente la charge tant que le bouton est maintenu
                Debug.Log("ca Charge");
                if (isCharging)
                {
                    chargeTime += Time.deltaTime;
                }

                // Quand on relâche, on lance l'attaque chargée si la charge est assez grande
                if (Firing)
                {
                    isCharging = false;
                    if (chargeTime >= _minChargeTime)
                    {
                        GameObject _bullet = Instantiate(_prfBullet, transform.position, Quaternion.identity);
                        _bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _bulletspeed);

                        // dégâts multiplier
                        Debug.Log("ca envoie x2");
                        _bullet.GetComponent<PlayerBulletScript>().Damage = _damage * _chargeMultiplier;
                    }

                    chargeTime = 0f;
                }
            }
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }
        
        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                DoFire();
            }
        }
    }
}