using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneChange : MonoBehaviour
{
   
    
    public void Start()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("SampleScene");
        }
   
        public void MainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        public void Quit()
        {
            Application.Quit();
        }
    
        public void FinalScene()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("FinalScene");
        }
        
   
}
