using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Infrastructure.Input
{
    public class InputHandler : IInputHandler, IDisposable
    {
        private readonly InputSystem _inputSystem;

        public event Action<Vector2> MoveButtonsPressed;
        public event Action<Vector2> RotateMousePressed;
        public event Action InteractionButtonReleased;
        public event Action AttackButtonReleased;

        public InputHandler()
        {
            _inputSystem = new InputSystem();
            _inputSystem.Enable();
        
            _inputSystem.Player.Move.performed += HandleMovementDirection;
            _inputSystem.Player.Move.canceled += HandleMovementDirection;

            _inputSystem.Player.Look.performed += HandleRotateDirection;
            _inputSystem.Player.Look.canceled += HandleRotateDirection;

            _inputSystem.Player.Interact.started += HandleInteractionButton;
            _inputSystem.Player.Attack.started += HandleAttackButton;
        }

        public void Dispose()
        {
            _inputSystem.Player.Move.performed -= HandleMovementDirection;
            _inputSystem.Player.Move.canceled -= HandleMovementDirection;
        
            _inputSystem.Player.Look.performed -= HandleRotateDirection;
            _inputSystem.Player.Look.canceled -= HandleRotateDirection;
        
            _inputSystem.Player.Interact.started -= HandleInteractionButton;
            _inputSystem.Player.Attack.started -= HandleAttackButton;
        
            _inputSystem.Disable();
        }

        private void HandleAttackButton(InputAction.CallbackContext obj)
        {
            AttackButtonReleased?.Invoke();
        }

        private void HandleInteractionButton(InputAction.CallbackContext obj)
        {
            InteractionButtonReleased?.Invoke();
        }

        private void HandleMovementDirection(InputAction.CallbackContext obj)
        {
            MoveButtonsPressed?.Invoke(obj.ReadValue<Vector2>());
        }

        private void HandleRotateDirection(InputAction.CallbackContext obj)
        {
            RotateMousePressed?.Invoke(obj.ReadValue<Vector2>());
        }
    }
}