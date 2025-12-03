using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public ParticleSystem onDestroy;
    private Collider2D _collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBlue"))
        {
            ParticleSystem clone = Instantiate(
                onDestroy,
                transform.position,
                onDestroy.transform.rotation
            );
            clone.Play();
            Destroy(clone.gameObject, 3);
            Destroy(gameObject);
        }
        if (other.CompareTag("PlayerRed"))
        {
            ParticleSystem clone = Instantiate(
                onDestroy,
                transform.position,
                onDestroy.transform.rotation
            );
            clone.Play();
            Destroy(clone.gameObject, 3);
            Destroy(gameObject);
        }
    }
}
