using __Workspaces.Jordan.Script.Ennemy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace __Workspaces.Jordan.Script.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Settings")]
        public int maxHealth = 10;
        public int currentHealth;
        public GameObject _gameObject;
        [SerializeField] private ColorType PlayerColor;
        
        //public HealthBar healthBar;
        private void Start()
        {
            currentHealth = maxHealth;
            // healthBar.SetMaxHealth(maxHealth);
        }
        public void TakeDamage(int damage)
        {
            Debug.Log("dégats");
            currentHealth -= damage;
            //healthBar.SetHealth(currentHealth);
            
            if (currentHealth <= 0)
            {
                Die(); 
            }
        }
        
        public void Die()
        {
            Debug.Log("Mort");
            Destroy(_gameObject);
            // Invoke("RestartLevel", 5);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Touchéb " + "Player Color = "+ PlayerColor  + " Collision Tag  = "+ collision.collider.transform.tag );
            //  Joueur Bleu
            if (PlayerColor == ColorType.Blue && collision.collider.CompareTag("BulletEnnemieBlue"))
            {
                TakeDamage(1);
            }
            Debug.Log("Touchér " + "Player Color = "+ PlayerColor  + " Collision Tag  = "+collision.collider.transform.tag );
            // Joueur Rouge
            if (PlayerColor == ColorType.Red && collision.collider.CompareTag("BulletEnnemieRed"))
            {
                TakeDamage(1);
            }
        }
    }
}