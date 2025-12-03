using UnityEngine;
using UnityEngine.InputSystem;

namespace __Workspaces.Jordan.Script.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] 
        private GameObject _prfBullet;
        [SerializeField] private float _speed;
        [SerializeField] private float _bulletspeed;
        [SerializeField] private int _damage = 2;
        private float _horizontal;
        private float _vertical;

        [Header("Inputs")] [SerializeField]
        public bool Firing;
        private Rigidbody2D _rb;
        public bool isCharging = false;
        public float chargeTime;
        public float _minChargeTime;
        public int _chargeMultiplier;
        public Vector2 Move;
        

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            DoLocomotion();
            DoFire();
            DoChargedFire();
        }

        private void DoLocomotion()
        {
            float horizontal = Move.x;
            float vertical = Move.y;
            float xMove = horizontal * Time.deltaTime * _speed;
            float yMove = vertical * Time.deltaTime * _speed;

            _rb.linearVelocity = new Vector2(xMove, yMove);
        }

        private void DoFire()
        {
            if (Firing)
            {
                GameObject instantiate = Instantiate(_prfBullet, transform.position, Quaternion.identity);
                instantiate.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _bulletspeed);
            } 
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
                        Debug.Log("ca envoie"); 
                        _bullet.GetComponent<BulletScript>().Damage = _damage * _chargeMultiplier; 
                    } 
                    chargeTime = 0f; 
                }
            }
        }
    }
}