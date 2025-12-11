using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneChange : MonoBehaviour
{
   
    
    public void Start()
        {
            //SceneManager.LoadScene("MenuPrincipale");
        }
   
        public void Start_level1()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("GameScene");
        }

        public void Quit()
        {
            Application.Quit();
            Debug.Log("Quit");
        }
    
        public void FinalScene()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("FinalScene");
        }
        
   
}
