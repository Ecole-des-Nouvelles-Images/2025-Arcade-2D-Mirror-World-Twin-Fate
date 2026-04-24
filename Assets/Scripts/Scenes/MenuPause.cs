using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class PauseMenu : MonoBehaviour
    {
        [Header("Pause Menu")]
        [SerializeField] private GameObject firstselectedbutton;
        [SerializeField] private GameObject MenuPause;

        private bool paused = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (paused)
                    Resume();
                else
                    Pause();
            }
        }
        public void OnPause(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed)
                return;

            TogglePause();
        }

        private void TogglePause()
        {
            if (paused)
                Resume();
            else
                Pause();
        }

        public void Resume()
        {
            MenuPause.SetActive(false);
            Time.timeScale = 1f;
            paused = false;
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Pause()
        {
            MenuPause.SetActive(true);
            Time.timeScale = 0f;
            paused = true;
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void MainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MenuPrincipal");
        }

        public void SettingsMenu()
        {
            SceneManager.LoadScene("SettingsMenu");
        }
    }
}
