using UnityEngine;

namespace __Workspaces.Jordan.Script
{
    public class GameStarter : MonoBehaviour
    {
        public GameObject BluePlayerPrefab;
        public GameObject RedPlayerPrefab;
        public Transform Spawn1;

        void Start()
        {
            // Instancie le joueur 1
            GameObject j1Prefab = PlayerSelection.J1Choice == CharacterChoice.Blue
                ? BluePlayerPrefab
                : RedPlayerPrefab;
            Instantiate(j1Prefab, Spawn1.position, Quaternion.identity);
            
            // Instancie le joueur 2
            GameObject j2Prefab = PlayerSelection.J2Choice == CharacterChoice.Blue
                ? BluePlayerPrefab
                : RedPlayerPrefab;
            Instantiate(j2Prefab, Spawn1.position, Quaternion.identity);
        }
    }
}