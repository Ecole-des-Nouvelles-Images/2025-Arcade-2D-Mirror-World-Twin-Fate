using System;
using __Workspaces.Jordan.Script.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __Workspaces.Jordan.Script
{
    public class InputHandler : MonoBehaviour
    {
        public static event Action<bool> OnInputDeviceChanged;
        
        private PlayerInput _playerInput;
        
        public PlayerController player;
        
        private bool _isControllerConnected;
        
        private void Awake()
        {
            player = FindFirstObjectByType<PlayerController>();
            _playerInput = GetComponent<PlayerInput>();
            if (_playerInput == null) throw new NullReferenceException("PlayerInputManager is null");
        }

        // private void OnEnable()
        // {
        //     InputSystem.onDeviceChange += OnDeviceChange;
        //     
        //     // _playerInput.actions["Move"].performed += Move;
        //     _playerInput.actions["Attack"].performed += Fire;
        // }
        //
        // private void OnDisable()
        // {
        //     InputSystem.onDeviceChange -= OnDeviceChange;
        //     
        //     // _playerInput.actions["Move"].canceled -= Move;
        //     _playerInput.actions["Attack"].canceled -= Fire;
        //
        // }
        //
        // private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        // {
        //     if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed) DetectCurrentInputDevice();
        // }
        //
        // private void DetectCurrentInputDevice()
        // {
        //     _isControllerConnected = Gamepad.all.Count > 0;
        //     OnInputDeviceChanged?.Invoke(_isControllerConnected);
        //
        //     Debug.Log(_isControllerConnected
        //         ? "Controller connected: Switching to Gamepad controls."
        //         : "No controller connected: Switching to Keyboard/Mouse controls.");
        // }
        //
        // // private void Move(InputAction.CallbackContext context)
        // // {
        // //     _playerInput = context.ReadValue<Vector2>();
        // // }
        //
        // // private void Fire(InputAction.CallbackContext context)
        // // {
        // //     _player.DoFire(context.ReadValue<float>());
        // // }
    }
}
