using Game.Scripts.GameLogic.CameraLogic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameLogic.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        private IPlayerMovement _playerMovement;
        private PlayerCameraRotation _playerCameraRotation;

        [Inject]
        private void Construct(IPlayerMovement playerMovement, PlayerCameraRotation playerCameraRotation)
        {
            _playerMovement = playerMovement;
            _playerCameraRotation = playerCameraRotation;
        }

        private void Update()
        {
            _playerMovement.Update(Time.deltaTime);
            _playerCameraRotation.Update(Time.deltaTime);
        }
    }
}