using UnityEngine;

public class ExplodingBullet : MonoBehaviour
{
    [Header("Bullet settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private int bulletsOnExplosion = 8;  // nombre de bullets générées à l'explosion
    [SerializeField] private GameObject bulletPrefab;     // le prefab de la petite balle
    [SerializeField] private float timer = 0f;
    
    private Vector2 direction;

    // Initialise la balle
    public void Init(Vector2 dir, float bulletSpeed)
    {
        direction = dir.normalized;
        speed = bulletSpeed;
        transform.up = direction;
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            Explode();
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        for (int i = 0; i < bulletsOnExplosion; i++)
        {
            float angle = 360f / bulletsOnExplosion * i;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.up; // cercle
            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bul = b.GetComponent<Bullet>();
            bul.speed = speed * 0.8f;  // légèrement plus lent pour les projectiles secondaires
            bul.bulletLife = 2f;
            b.transform.up = dir;
        }
    }
}