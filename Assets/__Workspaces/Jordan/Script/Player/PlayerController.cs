using UnityEngine;
using UnityEngine.InputSystem;

namespace __Workspaces.Jordan.Script.Player
{
    public class PlayerController : MonoBehaviour
    {
        
        [Header("Settings")]
        [SerializeField] private GameObject bullet;
        [SerializeField] private float speed;
        [SerializeField] private float damage = 2;
        private float _horizontal;
        private float _vertical;
        
        [Header("Inputs")]
        [SerializeField] private bool isFiring;
        
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            DoLocomotion();
            DoFire();
        }

        private  void DoLocomotion()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float xMove = horizontal * Time.deltaTime * speed;
            float yMove = vertical * Time.deltaTime * speed;
            
            _rb.linearVelocity = new Vector2(xMove, yMove);
        }
        public void DoFire()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject instantiate = Instantiate(bullet, transform.position, Quaternion.identity);
                instantiate.GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed);
            }
        }
        
        public void Move(InputAction.CallbackContext context)
        {
            _horizontal = context.ReadValue<float>();
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            isFiring = context.ReadValueAsButton();
        }
    }
}
