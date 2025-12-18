using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BulletSpawner : MonoBehaviour
{
    public event Action DestroyBullet;
    
    [Header("Bullet")] public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletLife = 3f;
    public bool DoFire = true;
    [SerializeField] private float _paternTime = 8f;     
    [Header("Paterne 1")] 
    [SerializeField] public int _wave = 10;
    [SerializeField] public int _bulletCount = 5;
    [SerializeField] public float _Spread = 35f;
    
    [Header("Paterne 2")]
    [SerializeField] public float _angle = 3f;
    [SerializeField] public int _wave2 = 20;
    
    [Header("Paterne 3")]
    [SerializeField] public int _shot = 3;
    [SerializeField] public float _time= 10;
    
    [Header("Paterne 4")]
    [SerializeField] public int _wave3 = 3;
    [SerializeField] public int _bulletCount2= 10;
    
    [Header("Paterne 5")]
    [SerializeField] public int _shot2 = 3;
    [SerializeField] public int _bulletCount3 = 3;
    [SerializeField] public float _angle2= 10;
    
    [Header("Paterne 6")]
    [SerializeField] public float  _SpeedBullet = 3;
    [SerializeField] public float _angle3 = 3;
    [SerializeField] public float _duration= 10f;
    
    private bool isAttacking = false;
    private float _timer;

    
    private void Update() {
        if (DoFire) {
            _timer += Time.deltaTime;
            if (_timer >= _paternTime) {
                int pattern = Random.Range(0, 6);
                StartCoroutine(PlayPattern(pattern));
                _timer = 0;
            }
        }
    }

    public void StopFire() {
        DoFire = false;
        DestroyBullet?.Invoke();
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



    // -------- PATTERNS ----------

    // Pattern 1 : Rafales droites
    private IEnumerator MultiStraightBurst()
    {
        int waves = _wave;
        int bulletCount = _bulletCount;
        float spread = _Spread;

        for (int i = 0; i < waves; i++)
        {
            for (int j = 0; j < bulletCount; j++)
            {
                float angle = Mathf.Lerp(-spread / 2f, spread / 2f, (float)j / (bulletCount - 1));
                Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.down;
                Shoot(dir);
            }

            if (!DoFire) yield return null;
            yield return new WaitForSeconds(0.15f);
        }
    }


    // Pattern 2 : Cercle qui tourne
    private IEnumerator SpinCircle()
    {
        float angle = _angle; // angle de départ
        int waves = _wave2; // nombre de vagues

        for (int i = 0; i < waves; i++)
        {

            float randomOffset = Random.Range(-5f, 5f);

            // ----- Couche interne -----
            int bulletsInner = 12;
            for (int j = 0; j < bulletsInner; j++)
            {
                float a = angle + (360f / bulletsInner) * j;
                Vector2 dir = Quaternion.Euler(0, 0, a) * Vector2.down;
                Shoot(dir);
            }

            // Avance l’angle pour la prochaine vague
            angle += 10f + randomOffset;

            // petite pause entre les vagues
            if (!DoFire) yield return null;
            yield return new WaitForSeconds(0.15f);
        }
    }

    // Pattern 3 : Mouvement en vague
    private IEnumerator Wave()
    {
        int shots = _shot;
        float time = _time;

        for (int i = 0; i < shots; i++)
        {
            float x = Mathf.Sin(time) * 0.7f;
            Vector2 dir = new Vector2(x, -1f).normalized;

            Shoot(dir);
            time += 0.2f;
            if (!DoFire) yield return null;
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Pattern 4 : Pluie aléatoire
    private IEnumerator RandomRain()
    {
        int waves = _wave3;
        int bulletsPerWave = _bulletCount2;

        for (int i = 0; i < waves; i++)
        {
            for (int j = 0; j < bulletsPerWave; j++)
            {
                float randomX = Random.Range(-20f, 20f);
                Vector3 pos = new Vector3(randomX, firePoint.position.y, 0f);

                GameObject b = Instantiate(bulletPrefab, pos, Quaternion.identity);
                SetupBullet(b, Vector2.down);
            }

            if (!DoFire) yield return null;
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Pattern 5 : Tirs visés vers le joueur
    private IEnumerator AimedShotsCone()
    {
        int shots = _shot2;
        int bulletsPerShot = _bulletCount3;
        float coneAngle = _angle2;

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
            if (!DoFire) yield return null;
            yield return new WaitForSeconds(0.25f);
        }
    }

    //pattern 6 : souffle dragon
    private IEnumerator FireBreathSweep()
    {
        float duration = _duration;       
        float coneAngle = _angle3;       
        float elapsed = 0f;
        float speedMultiplier = _SpeedBullet; // un petit boost de vitesse

        while (elapsed < duration)
        {
            int bulletsThisFrame = 5;

            for (int i = 0; i < bulletsThisFrame; i++)
            {
                Vector2 baseDir = Vector2.down;
                float angleOffset = Random.Range(-coneAngle / 2f, coneAngle / 2f);
                Vector2 dir = Quaternion.Euler(0, 0, angleOffset) * baseDir;

                ShootWithSpeed(dir, speedMultiplier); // utilise la nouvelle fonction
            }
            if (!DoFire) yield return null;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }






    private void Shoot(Vector2 direction)
    {
        GameObject b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Bullet bul = b.GetComponent<Bullet>();

        bul.bulletLife = bulletLife;
        b.transform.up = direction;
        bul.SetUpBullet(this);
    }


    private void SetupBullet(GameObject b, Vector2 dir)
    {
        Bullet bul = b.GetComponent<Bullet>();

        bul.bulletLife = bulletLife;
        b.transform.up = dir;
    }

    private void ShootWithSpeed(Vector2 direction, float extraSpeed)
    {
        GameObject b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bul = b.GetComponent<Bullet>();

        bul.bulletLife = bulletLife;
        bul.speed += extraSpeed; // ajoute un peu de vitesse

        b.transform.up = direction;
    }
}

