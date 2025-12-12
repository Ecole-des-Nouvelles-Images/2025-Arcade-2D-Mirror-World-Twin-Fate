using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ControllerRumble : MonoBehaviour
{
    private Coroutine rumbleCoroutine;
    
    public void Rumble(float lowFreq, float highFreq, float duration)
    {
        if (Gamepad.current == null)
            return;
        
        if (rumbleCoroutine != null)
        {
            StopCoroutine(rumbleCoroutine);
        }

        Gamepad.current.SetMotorSpeeds(lowFreq, highFreq);
        
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

        if (Gamepad.current != null)
            Gamepad.current.SetMotorSpeeds(0f, 0f);
    }
    private IEnumerator RumbleTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        StopRumble();
    }
}