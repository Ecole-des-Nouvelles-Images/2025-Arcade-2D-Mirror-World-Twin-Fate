using UnityEngine;
using UnityEngine.SceneManagement;

namespace __Workspaces.Jordan.Script
{
    public enum CharacterChoice
    {
        Blue,
        Red
    }

    public class PlayerSelection : MonoBehaviour
    {
        //static pour sauvergarder les choix jusqu'a la scène de jeu
        public static CharacterChoice J1Choice;
        public static CharacterChoice J2Choice;

        private bool j1Chosen = false;
        private bool j2Chosen = false;

        private bool blueAvailable = true;
        private bool redAvailable = true;

        // si Bleu est sélectionné par l'un des deux joueurs
        public void SelectBlue()
        {
            if (!blueAvailable) 
            { 
                Debug.Log("Le bleu plus disponible !");
                return;
            }

            if (!j1Chosen)
            {
                J1Choice = CharacterChoice.Blue;
                j1Chosen = true;
            }
            else if (!j2Chosen)
            {
                J2Choice = CharacterChoice.Blue;
                j2Chosen = true;
            }

            blueAvailable = false; // si bleu choisi alors bleu plus disponible
        }

        // si rouge est sélectionné par l'un des deux joueurs
        public void SelectRed()
        {
            if (!redAvailable) 
            { 
                Debug.Log("Le rouge plus disponible !");
                return;
            }

            if (!j1Chosen)
            {
                J1Choice = CharacterChoice.Red;
                j1Chosen = true;
            }
            else if (!j2Chosen)
            {
                J2Choice = CharacterChoice.Red;
                j2Chosen = true;
            }

            redAvailable = false; // si rouge choisi alors rouge plus disponible
        }
        
        public void StartGame(string sceneName)
        {
            if (!j1Chosen || !j2Chosen)
            {
                Debug.Log("Les deux joueurs doivent choisir un personnage !");
                return;
            }

            SceneManager.LoadScene(sceneName);
        }
    }
}
