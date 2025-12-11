using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletSpawner : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 15f;
    public float bulletLife = 3f;

    [Header("Boss Life")]
    public float maxLife = 100f;
    private float currentLife;

    private bool isPhase2 = false;
    private bool isAttacking = false;

    private void Start()
    {
        currentLife = maxLife;
        StartCoroutine(BossLoop());
    }

    private IEnumerator BossLoop()
    {
        while (true)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                int pattern = Random.Range(0, 7);
                yield return StartCoroutine(PlayPattern(pattern));
                yield return new WaitForSeconds(0.5f); // repos entre les patterns
                isAttacking = false;
            }

            yield return null;
        }
    }

    private IEnumerator PlayPattern(int pattern)
    {
        switch (pattern)
        {
            case 0:
                yield return StartCoroutine(MultiStraightBurst());
                break;
            case 1:
                yield return StartCoroutine(SpinCircle());
                break;
            case 2:
                yield return StartCoroutine(Wave());
                break;
            case 3:
                yield return StartCoroutine(RandomRain());
                break;
            case 4:
                yield return StartCoroutine(AimedShotsCone());
                break;
            case 5:
                yield return StartCoroutine(FireBreathSweep());
                break;
        }
    }
    
    

    // -------- PHASE CHECK ----------
    private void CheckPhase()
    {
        if (!isPhase2 && currentLife <= maxLife * 0.5f)
        {
            isPhase2 = true;
        }
    }

    // -------- PATTERNS ----------

    // Pattern 1 : Rafales droites
    private IEnumerator MultiStraightBurst()
    {
        int waves = isPhase2 ? 15 : 10;
        int bulletCount = isPhase2 ? 10 : 6;
        float spread = isPhase2 ? 26f : 25f;

        for (int i = 0; i < waves; i++)
        {
            for (int j = 0; j < bulletCount; j++)
            {
                float angle = Mathf.Lerp(-spread/2f, spread/2f, (float)j / (bulletCount - 1));
                Vector2 dir = Quaternion.Euler(0,0,angle) * Vector2.down;
                Shoot(dir);
            }

            yield return new WaitForSeconds(0.15f);
        }
    }


    // Pattern 2 : Cercle qui tourne
    private IEnumerator SpinCircle()
    {
        float angle = 0f;                           // angle de départ
        int waves = isPhase2 ? 15 : 12;             // nombre de vagues

        for (int i = 0; i < waves; i++)
        {
            // petite variation aléatoire pour la rotation
            float randomOffset = Random.Range(-5f, 5f);

            // ----- Couche interne -----
            int bulletsInner = isPhase2 ? 14 : 12;
            for (int j = 0; j < bulletsInner; j++)
            {
                float a = angle + (360f / bulletsInner) * j;
                Vector2 dir = Quaternion.Euler(0, 0, a) * Vector2.down;
                Shoot(dir);
            }

            // Avance l’angle pour la prochaine vague
            angle += (isPhase2 ? 15f : 10f) + randomOffset;

            // petite pause entre les vagues
            yield return new WaitForSeconds(isPhase2 ? 0.12f : 0.15f);
        }
    }

    // Pattern 3 : Mouvement en vague
    private IEnumerator Wave()
    {
        int shots = isPhase2 ? 40 : 25;
        float time = 0f;

        for (int i = 0; i < shots; i++)
        {
            float x = Mathf.Sin(time) * 0.7f;
            Vector2 dir = new Vector2(x, -1f).normalized;

            Shoot(dir);
            time += isPhase2 ? 0.3f : 0.2f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Pattern 4 : Pluie aléatoire
    private IEnumerator RandomRain()
    {
        int waves = isPhase2 ? 60 : 40;          // nombre de vagues
        int bulletsPerWave = isPhase2 ? 4 : 2;   // projectiles par vague

        for (int i = 0; i < waves; i++)
        {
            for (int j = 0; j < bulletsPerWave; j++)
            {
                float randomX = Random.Range(-20f, 20f);
                Vector3 pos = new Vector3(randomX, firePoint.position.y, 0f);

                GameObject b = Instantiate(bulletPrefab, pos, Quaternion.identity);
                SetupBullet(b, Vector2.down);
            }

            yield return new WaitForSeconds(isPhase2 ? 0.05f : 0.1f);
        }
    }

    // Pattern 5 : Tirs visés vers le joueur
    private IEnumerator AimedShotsCone()
    {
        int shots = isPhase2 ? 12 : 8;          // nombre de salves
        int bulletsPerShot = isPhase2 ? 5 : 3;  // balles par salve
        float coneAngle = 40f;                  // angle de l'éventail

        GameObject player = GameObject.FindGameObjectWithTag("PlayerBlue");

        for (int i = 0; i < shots; i++)
        {
            if (player != null)
            {
                Vector2 baseDir = ((Vector2)player.transform.position - (Vector2)firePoint.position).normalized;

                for (int j = 0; j < bulletsPerShot; j++)
                {
                    float t = bulletsPerShot <= 1 ? 0 : (float)j / (bulletsPerShot - 1);
                    float angleOffset = Mathf.Lerp(-coneAngle / 2f, coneAngle / 2f, t);
                    Vector2 dir = Quaternion.Euler(0, 0, angleOffset) * baseDir;
                    Shoot(dir);
                }
            }

            yield return new WaitForSeconds(isPhase2 ? 0.15f : 0.25f);
        }
    }
    
    //pattern 6 : souffle dragon
    private IEnumerator FireBreathSweep()
    {
        float duration = isPhase2 ? 2.5f : 3.5f;

        float startCone = 5f;                     // cône très serré au début
        float endCone = isPhase2 ? 10f : 5f;     // cône large à la fin

        float elapsed = 0f;

        // On prend la dernière position du joueur
        GameObject player = GameObject.FindGameObjectWithTag("PlayerBlue");
        Vector2 targetPos = player != null ? (Vector2)player.transform.position : Vector2.down;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // Ouverture progressive du cône
            float coneAngle = Mathf.Lerp(startCone, endCone, t);

            // Direction de base vers le joueur
            Vector2 baseDir = (targetPos - (Vector2)firePoint.position).normalized;

            // Quantité de balles par frame (pluie massive)
            int bulletsThisFrame = isPhase2 ? 3 : 1;

            for (int i = 0; i < bulletsThisFrame; i++)
            {
                // Angle aléatoire dans le cône
                float angle = Random.Range(-coneAngle / 2f, coneAngle / 2f);

                // Direction finale du projectile
                Vector2 dir = Quaternion.Euler(0, 0, angle) * baseDir;

                // Tir
                Shoot(dir);
            }

            elapsed += Time.deltaTime;
            yield return null; // chaque frame
        }
    }



    // --------- SHOOT UTIL ----------
    private void Shoot(Vector2 direction)
    {
        GameObject b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Bullet bul = b.GetComponent<Bullet>();
        bul.speed = isPhase2 ? bulletSpeed * 1.3f : bulletSpeed;
        bul.bulletLife = bulletLife;

        // Oriente la balle dans la bonne direction
        b.transform.up = direction;
    }


    private void SetupBullet(GameObject b, Vector2 dir)
    {
        Bullet bul = b.GetComponent<Bullet>();
        bul.speed = isPhase2 ? bulletSpeed * 1.3f : bulletSpeed;
        bul.bulletLife = bulletLife;
        b.transform.up = dir;
    }

    // -------- EXEMPLE : dommages boss --------
    public void TakeDamage(float dmg)
    {
        currentLife -= dmg;
        CheckPhase();

        if (currentLife <= 0)
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}
