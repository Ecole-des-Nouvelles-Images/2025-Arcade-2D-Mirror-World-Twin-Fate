using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class sceneChange : MonoBehaviour
    {
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
        public void MainMenu()
        {
            SceneManager.LoadScene("MenuPrincipal");
        }

    
        public void FinalScene()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("FinalScene");
        }
        
   
    }
}
