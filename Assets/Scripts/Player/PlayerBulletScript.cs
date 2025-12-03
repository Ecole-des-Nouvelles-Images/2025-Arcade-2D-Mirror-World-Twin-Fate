using UnityEngine;

namespace Player
{
    public class PlayerBulletScript : MonoBehaviour
    {
        [SerializeField] private int _damage;
        private Rigidbody2D rb;
        [SerializeField] private float _delayToDestroy;
        public ParticleSystem destroy;

        public int Damage {
            get => _damage;
            set => _damage = value;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, _delayToDestroy);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnDestroy();

            Debug.Log("il subit des dégats");

            if (collision.gameObject.CompareTag("EnnemieRed") || collision.gameObject.CompareTag("EnnemieBlue"))
            {
                Destroy(gameObject);
            }

        }
        private void OnDestroy()
        {
            if (destroy != null)
            {
                ParticleSystem clone = Instantiate(
                    destroy,
                    transform.position,
                    destroy.transform.rotation
                );
                clone.Play();
                Destroy(clone.gameObject, 3);
            }
        }
    }
}

