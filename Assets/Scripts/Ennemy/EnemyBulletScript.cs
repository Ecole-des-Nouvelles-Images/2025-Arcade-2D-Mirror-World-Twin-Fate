using UnityEngine;

namespace Ennemies
{
    public class EnemyBulletScript : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _delayToDestroy;
        [SerializeField] ParticleSystem destroy;
       
        private Rigidbody2D rb;
        public int SetDamage(int damage) => _damage = damage;
        public int GetDamage() => _damage;
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, _delayToDestroy);
        }
        private void OnTriggerEnter2D(Collider2D other)
        { 
            if (other.gameObject.CompareTag("PlayerBlue") || other.gameObject.CompareTag("PlayerRed")) 
            {
                DoVFX();
                Destroy(gameObject); 
            }
            else if (other.CompareTag("BulletDestroyer"))
            {
                {
                    DestroyBulletNoVFX();
                }
            }
        }
        private void DoVFX()
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
        private void DestroyBulletNoVFX()
        {
            Destroy(gameObject);
        }
    }
}