// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;
//
// namespace __Workspaces.Baptiste.scripts
// {
//     public class HealthBar : MonoBehaviour
//     {
//         [SerializeField] private GameObject _healthBar;
//         [SerializeField] private Text _PlayerBlue;
//         [SerializeField] private Text _PlayerRed;
//         [SerializeField] private float health = 100;
//         private float maxHealth = 100;
//     
//         private Rigidbody2D _rigidBody;
//         public Image healthBar;
//         void Update()
//         {
//             healthBar.fillAmount = health / maxHealth;
//
//
//             if (health <= 0)
//             {
//                 SceneManager.LoadScene("GameOver");
//             }
//         
//         }
//
//         private void OnTriggerEnter2D(Collider2D collision)
//         {
//             if (collision.CompareTag("BulletEnnemieBlue") )
//             {
//                 Debug.Log("Damage with " + collision.name);
//                 TakeDamage(50);
//             }
//
//             void TakeDamage(float damage)
//             {
//                 health -= damage;
//                 healthBar.fillAmount = health / 100f;
//             }
//         }
//
//     }
// }
//
//
//         public static SharedHealth instance;
//
//         public float currentHealth;
//
//         private void Awake()
//         {
//             instance = this;
//         }
//
//         void Start()
//         {
//             currentHealth = maxHealth;
//             UpdateHealthBar();
//         }
//
//         public void TakeDamage(float damage)
//         {
//             currentHealth -= damage;
//             currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
//
//             UpdateHealthBar();
//
//             if (currentHealth <= 0)
//             {
//                 SceneManager.LoadScene("GameOver");
//             }
//         }
//
//         void UpdateHealthBar()
//         {
//             if (healthBar != null)
//                 healthBar.fillAmount = currentHealth / maxHealth;
//         }
//     }
// }
//
//     
