using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class ControllerSelection : MonoBehaviour
{
    // Stocke les PlayerInput (chacun est déjà pairé à une manette)
    public List<PlayerInput> joinedPlayers = new List<PlayerInput>();

    private PlayerInputManager pim;

    private void Awake()
    {
        pim = FindObjectOfType<PlayerInputManager>();
    }

    // Lier cette méthode à l'event onPlayerJoined du PlayerInputManager
    public void OnPlayerJoined(PlayerInput player)
    {
        if (!joinedPlayers.Contains(player))
        {
            joinedPlayers.Add(player);
            Debug.Log($"Player joined: {player.devices.Count} device(s) - {player.devices[0].displayName}");
        }

        // Quand on a 2 joueurs, on arrête le join et on charge la scène de jeu
        if (joinedPlayers.Count >= 2)
        {
            // 1) empêcher de nouveaux join (important pour éviter l'erreur "playerPrefab must be set...")
            if (pim != null) pim.enabled = false;

            // 2) conserver cet objet entre les scènes
            DontDestroyOnLoad(this.gameObject);

            // 3) charger la scène de jeu (ajoute la scène dans Build Settings)
            SceneManager.LoadScene("GameScene"); // nom exact de ta scene
        }
    }
}
