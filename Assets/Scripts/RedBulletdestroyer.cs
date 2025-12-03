using UnityEngine;

public class RedBulletdestroyer : MonoBehaviour
{
    [SerializeField] private int _damage;
    public ParticleSystem destroy;
    [SerializeField] private float _delayToDestroy = 3;

    public int Damage {
        get => _damage;
        set =>_damage=value;
    }


    private void Start()
    {
        Destroy(gameObject, _delayToDestroy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnnemieRed"));
        {
            //Debug.Log("EnnemieRed");
            ParticleSystem clone = Instantiate(
                destroy,
                transform.position,
                destroy.transform.rotation
            );
            clone.Play();
            Destroy(clone.gameObject, 3);
            Destroy(gameObject);
        }
    }
}

