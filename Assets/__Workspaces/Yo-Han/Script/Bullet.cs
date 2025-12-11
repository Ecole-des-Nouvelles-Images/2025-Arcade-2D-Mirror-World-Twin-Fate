using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 1f;
    public float speed = 5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= bulletLife)
            Destroy(gameObject);

        transform.position += transform.up * speed * Time.deltaTime;
    }
}