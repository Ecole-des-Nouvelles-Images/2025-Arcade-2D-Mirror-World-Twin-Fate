using System;
using UnityEngine;

namespace __Workspaces.Jordan.Script.Player
{
    public class BulletScript : MonoBehaviour
    {
        [SerializeField] private int _damage;
        private Rigidbody2D rb;
        [SerializeField] private float _delayToDestroy = 2;

        public int Damage {
            get => _damage;
            set => _damage = value;
        }
        
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, _delayToDestroy);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("il subit des dégats");
            if (collision.gameObject.CompareTag("EnnemieRed"))
            { 
                Destroy(gameObject);
            }
        }
    }
}
