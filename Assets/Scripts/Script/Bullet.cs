using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 1f;
    public float speed = 5f;

    private float timer;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        bulletLife -= Time.deltaTime;
        if (bulletLife <= 0f) Destroy(gameObject);
    }
}