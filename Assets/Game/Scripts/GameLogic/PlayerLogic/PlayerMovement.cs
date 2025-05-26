using System;
using Game.Scripts.GameLogic.Player.GameData.Player;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.Scripts.GameLogic.Player
{
    public class PlayerMovement : IPlayerMovement, IUpdatable, IDisposable
    {
        private readonly CharacterController _characterController;
        private readonly IInputHandler _inputHandler;
        private readonly PlayerCharacteristicData _playerCharacteristicData;
        private readonly CinemachineCamera _camera;
        private readonly float _gravity = -9.81f;
        
        private Vector2 _moveInput;
        private float _verticalVelocity = 0f;
        private bool _isGrounded;

        public PlayerMovement(CharacterController characterController, IInputHandler inputHandler, PlayerCharacteristicData playerCharacteristicData, CinemachineCamera camera)
        {
            _characterController = characterController;
            _inputHandler = inputHandler;
            _playerCharacteristicData = playerCharacteristicData;
            _camera = camera;
            
            _inputHandler.MoveButtonsPressed += ReadMovementVector;
        }
        
        public bool IsMoving => _moveInput != Vector2.zero;

        public void Update(float deltaTime)
        {
            _isGrounded = _characterController.isGrounded;
            
            if (_isGrounded && _verticalVelocity < 0)
            {
                _verticalVelocity = -2f;
            }
            
            Move(deltaTime);
        }

        public void Dispose()
        {
            _inputHandler.MoveButtonsPressed -= ReadMovementVector;
        }

        private void ReadMovementVector(Vector2 movementVector)
        {
            _moveInput = movementVector;
        }

        private void Move(float deltaTime)
        {
            Vector3 move = new Vector3(_moveInput.x, 0, _moveInput.y);
            move = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * move;
            move.Normalize();
            move *= _playerCharacteristicData.Speed;
            _verticalVelocity += _gravity * deltaTime;
            move.y = _verticalVelocity;
            _characterController.Move(move * deltaTime);
        }
    }
}