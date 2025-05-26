using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : IInputHandler, IDisposable
{
    private readonly InputSystem _inputSystem;

    public event Action<Vector2> MoveButtonsPressed;
    public event Action<Vector2> RotateMousePressed;

    public InputHandler()
    {
        _inputSystem = new InputSystem();
        _inputSystem.Enable();
        
        _inputSystem.Player.Move.performed += HandleMovementDirection;
        _inputSystem.Player.Move.canceled += HandleMovementDirection;

        _inputSystem.Player.Look.performed += HandleRotateDirection;
        _inputSystem.Player.Look.canceled += HandleRotateDirection;
    }

    public void Dispose()
    {
        _inputSystem.Player.Move.performed -= HandleMovementDirection;
        _inputSystem.Player.Move.canceled -= HandleMovementDirection;
        
        _inputSystem.Player.Look.performed -= HandleRotateDirection;
        _inputSystem.Player.Look.canceled -= HandleRotateDirection;
        
        _inputSystem.Disable();
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