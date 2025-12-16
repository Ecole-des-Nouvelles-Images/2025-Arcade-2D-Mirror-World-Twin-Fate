using UnityEngine;

namespace Player
{
    public class PlayerChargedBulletScript : MonoBehaviour
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
            if (gameObject.CompareTag("BulletBlue") && collision.gameObject.CompareTag("EnnemieRed"))
            {
                DoVFX();
                Destroy(gameObject);
                
            }
            if (gameObject.CompareTag("BulletRed") && collision.gameObject.CompareTag("EnnemieBlue"))
            {
                DoVFX();
                Destroy(gameObject);
                
            }
        }
        private void DoVFX() {
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

