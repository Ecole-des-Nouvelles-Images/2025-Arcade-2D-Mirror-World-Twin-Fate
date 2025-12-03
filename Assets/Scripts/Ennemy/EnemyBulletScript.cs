using UnityEngine;

namespace Ennemy
{
    public class EnemyBulletScript : MonoBehaviour
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnDestroy();

            Debug.Log("il subit des dégats");

            // if (other.gameObject.CompareTag("EnnemieRed") || other.gameObject.CompareTag("EnnemieBlue"))
            // {
            //     Destroy(gameObject);
            // }
           if (other.gameObject.CompareTag("PlayerBlue") || other.gameObject.CompareTag("PlayerRed"))
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