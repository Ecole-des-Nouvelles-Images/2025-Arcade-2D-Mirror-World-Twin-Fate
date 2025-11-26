using UnityEngine;

namespace __Workspaces.Jordan.Script.Player
{
    public class BulletScript : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float bulletForce;
        [SerializeField] private float _delayToDestroy = 3;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, _delayToDestroy);
        }

        public void Fire(Vector2 direction)
        {
            rb.AddForce(direction * bulletForce);
        }
    }
}
