using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ControllerRumble : MonoBehaviour
{
    private Coroutine rumbleCoroutine;
    private Gamepad gamepad;
    private PlayerInput playerInput;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        foreach (var device in playerInput.devices)
        {
            if (device is Gamepad pad)
            {
                gamepad = pad;
                break;
            }
        }
    }

    public void Rumble(float lowFreq, float highFreq, float duration)
    {
        if (gamepad == null)
            return;
        
        if (rumbleCoroutine != null)
        {
            StopCoroutine(rumbleCoroutine);
        }

        gamepad.SetMotorSpeeds(lowFreq, highFreq);
        
        if (duration > 0)
        {
            rumbleCoroutine = StartCoroutine(RumbleTimer(duration));
        }
    }
    public void StopRumble()
    {
        if (rumbleCoroutine != null)
            StopCoroutine(rumbleCoroutine);

        rumbleCoroutine = null;

        if (gamepad != null)
            gamepad.SetMotorSpeeds(0f, 0f);
    }
    private IEnumerator RumbleTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        StopRumble();
    }
    private void OnDisable()
    {
        StopRumble();
    }
}