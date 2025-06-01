using System;
using Game.Scripts.Infrastructure;
using Game.Scripts.Infrastructure.GameData.Player;
using Game.Scripts.Infrastructure.Input;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.Scripts.GameLogic.PlayerLogic
{
    public class PlayerCameraRotation : IUpdatable, IDisposable
    {
        private const float MaxRotationX = 80f;
        private const float MinRotationX = -80f;
        
        private readonly IInputHandler _inputHandler;
        private readonly PlayerCharacteristicData _playerCharacteristicData;
        private readonly CinemachineCamera _camera;
        
        private Vector2 _rotateDirection;
        private float _xRotation;
        
        public PlayerCameraRotation(IInputHandler inputHandler, PlayerCharacteristicData playerCharacteristicData, CinemachineCamera camera)
        {
            _inputHandler = inputHandler;
            _camera = camera;
            _playerCharacteristicData  = playerCharacteristicData;
            
            _inputHandler.RotateMousePressed += ReadRotateVector;
        }

        public void Update(float deltaTime)
        {
            Rotate(deltaTime);
        }

        private void Rotate(float deltaTime)
        {
            float mouseX = _rotateDirection.x * _playerCharacteristicData.MouseSensitivityX;
            float mouseY = _rotateDirection.y * _playerCharacteristicData.MouseSensitivityY;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, MinRotationX, MaxRotationX);

            _camera.transform.localEulerAngles = new Vector3(_xRotation, _camera.transform.localEulerAngles.y + mouseX, 0f);
        }

        public void Dispose()
        {
            _inputHandler.RotateMousePressed -= ReadRotateVector;
        }

        private void ReadRotateVector(Vector2 moveRotation)
        {
            _rotateDirection = moveRotation;
        }
    }
}