using System;
using UnityEngine;

public interface IInputHandler
{
    public event Action<Vector2> MoveButtonsPressed;
    public event Action<Vector2> RotateMousePressed;
    public event Action InteractionButtonReleased;
}