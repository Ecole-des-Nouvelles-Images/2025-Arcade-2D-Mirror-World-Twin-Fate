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

        private void OnEnable()
        {
            InputSystem.onDeviceChange += OnDeviceChange;
            
            _playerInput.actions["Move"].performed += OnMove;
            _playerInput.actions["Attack"].performed += OnFire;
            
            _playerInput.actions["Move"].canceled += OnMove;
            _playerInput.actions["Attack"].canceled += OnFire;
        }
        
        private void OnDisable()
        {
            InputSystem.onDeviceChange -= OnDeviceChange;

            _playerInput.actions["Move"].performed -= OnMove;
            _playerInput.actions["Attack"].performed -= OnFire;
            
            _playerInput.actions["Move"].canceled -= OnMove;
            _playerInput.actions["Attack"].canceled -= OnFire;
        
        }
        
        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed) DetectCurrentInputDevice();
        }
        
        private void DetectCurrentInputDevice()
        {
            _isControllerConnected = Gamepad.all.Count > 0;
            OnInputDeviceChanged?.Invoke(_isControllerConnected);
        
            Debug.Log(_isControllerConnected
                ? "Controller connected: Switching to Gamepad controls."
                : "No controller connected: Switching to Keyboard/Mouse controls.");
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            player.Move = context.ReadValue<Vector2>();
        }
        
        public void OnFire(InputAction.CallbackContext context)
        {
            player.Firing = context.ReadValue<float>() > 0;
        }
    }
}