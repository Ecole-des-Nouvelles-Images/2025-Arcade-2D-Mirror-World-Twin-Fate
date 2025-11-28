using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public float health = 100;
    public float maxHealth = 100;
    
    private Rigidbody2D _rigidBody;
    
    public Image healthBar;
    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / maxHealth;


        if (healthBar.fillAmount <= 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemy"))
        {
            Debug.Log("Damage with " + collision.name);
            TakeDamage(5);
        }

        void TakeDamage(float damage)
        {
            health -= damage;
            healthBar.fillAmount = health / 100f;
        }
    }

}
    
    
