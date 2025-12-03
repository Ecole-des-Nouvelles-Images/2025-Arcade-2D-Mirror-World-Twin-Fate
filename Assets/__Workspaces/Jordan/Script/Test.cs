using UnityEngine;
using UnityEngine.InputSystem;

namespace __Workspaces.Jordan.Script
{
    public class Test : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private GameObject bullet;
        [SerializeField] private float speed;
        [SerializeField] private float bulletspeed;
        [SerializeField] private float damage = 2;
        private Vector2 Move;
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            //Bouger avec le transform (plus doux)
            //transform.Translate(Move * speed * Time.deltaTime);
            //bouger avec le rigidbody (plus brutal)
            _rb.linearVelocity = new Vector2(Move.x * speed, Move.y * speed);
        }

        public void DoFire()
        {
            GameObject instantiate = Instantiate(bullet, transform.position, Quaternion.identity);
            instantiate.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bulletspeed);
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
