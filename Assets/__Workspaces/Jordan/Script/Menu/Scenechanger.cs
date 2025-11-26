using UnityEngine;
using UnityEngine.SceneManagement;

namespace __Workspaces.Jordan.Script
{
    public class SceneChanger : MonoBehaviour
    {

        public string NextSceneName;
    
        public void LoadScene()
        {
            SceneManager.LoadScene(NextSceneName);
        }
    }
}
