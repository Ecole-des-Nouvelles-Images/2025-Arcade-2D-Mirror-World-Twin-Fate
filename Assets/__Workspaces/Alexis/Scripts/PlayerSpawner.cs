using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerSpawner : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public Vector3 spawnP1 = new Vector3(-2, 0, 0);
    public Vector3 spawnP2 = new Vector3(2, 0, 0);

    void Start()
    {
        StartCoroutine(SpawnPlayersNextFrame());
    }

    private IEnumerator SpawnPlayersNextFrame()
    {
        yield return null; // attend 1 frame pour que les devices soient assignés

        var selector = FindObjectOfType<ControllerSelection>();
        if (selector == null || selector.joinedPlayers.Count < 2)
        {
            Debug.LogError("Pas assez de joueurs détectés !");
            yield break;
        }

        if (selector.joinedPlayers[0].devices.Count == 0 || selector.joinedPlayers[1].devices.Count == 0)
        {
            Debug.LogError("Un joueur n'a aucun device assigné !");
            yield break;
        }

        var device1 = selector.joinedPlayers[0].devices[0];
        var device2 = selector.joinedPlayers[1].devices[0];

        var p1 = Instantiate(player1Prefab, spawnP1, Quaternion.identity);
        var p2 = Instantiate(player2Prefab, spawnP2, Quaternion.identity);

        var input1 = p1.GetComponent<PlayerInput>();
        var input2 = p2.GetComponent<PlayerInput>();

        input1.SwitchCurrentControlScheme(input1.currentControlScheme, device1);
        input2.SwitchCurrentControlScheme(input2.currentControlScheme, device2);
    }
}
