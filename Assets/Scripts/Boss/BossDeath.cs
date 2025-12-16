using UnityEngine;
using System.Collections;
public class BossDeath : MonoBehaviour
{
    public ParticleSystem _deathEffect;
    public int explosionCount = 8;
    public float delayBetweenExplosions = 0.2f;
    private Collider2D bossCollider;
    void Awake()
    {
        bossCollider = GetComponent<Collider2D>();
    }
    public void Die()
    {
        StartCoroutine(ExplosionSequence());
    }

    IEnumerator ExplosionSequence()
    {
        for (int i = 0; i < explosionCount; i++)
        {
            Vector2 randomPos = GetRandomPointInBounds(bossCollider.bounds);

            ParticleSystem clone = Instantiate(_deathEffect, randomPos, Quaternion.identity);
            clone.Play();
            Destroy(clone.gameObject, 3);
            
            yield return new WaitForSeconds(delayBetweenExplosions);
        }
    }

    Vector2 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
}
