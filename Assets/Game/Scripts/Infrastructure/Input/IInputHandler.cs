using System;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Input
{
    public interface IInputHandler
    {
        public event Action<Vector2> MoveButtonsPressed;
        public event Action<Vector2> RotateMousePressed;
        public event Action InteractionButtonReleased;
        public event Action AttackButtonReleased;

        public void Deactivate();
        public void Activate();
    }
}