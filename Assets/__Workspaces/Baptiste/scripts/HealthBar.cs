using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
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


        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletEnnemieBlue") )
        {
            Debug.Log("Damage with " + collision.name);
            TakeDamage(50);
        }

        void TakeDamage(float damage)
        {
            health -= damage;
            healthBar.fillAmount = health / 100f;
        }
    }

}
    
