using UnityEngine;

public class SpiderTir : MonoBehaviour
{
    [Header("References")]
    public Transform gun;             
    public GameObject bulletPrefab;    

    [Header("Shooting Settings")]
    public int bulletCount = 7;         // Nombre de bullets dans l’éventail
    public float spreadAngle = 60f;     // Angle total de l’éventail
    public float fireRate = 1.5f;       // Temps entre chaque tir
    public float bulletSpeed = 5f;      // Vitesse des bullets

    private float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            ShootFan();
            fireTimer = 0f;
        }
    }

    void ShootFan()
    {
        if (bulletCount <= 0) return;

        float angleStep = spreadAngle / (bulletCount - 1);
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + angleStep * i;

            // Direction de base : vers le bas (shoot’em up vertical)
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.down;

            GameObject bullet = Instantiate(
                bulletPrefab,
                gun.position,
                Quaternion.identity
            );

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction.normalized * bulletSpeed;
            }
        }
    }
}

