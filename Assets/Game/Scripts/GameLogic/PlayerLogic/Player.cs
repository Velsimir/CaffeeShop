using Game.Scripts.GameLogic.CameraLogic;
using Game.Scripts.GameLogic.PlayerLogic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameLogic.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        private IPlayerMovement _playerMovement;
        private PlayerCameraRotation _playerCameraRotation;
        private InteractionMediator _interactionMediator;

        [Inject]
        private void Construct(IPlayerMovement playerMovement, PlayerCameraRotation playerCameraRotation, InteractionMediator interactionMediator)
        {
            _playerMovement = playerMovement;
            _playerCameraRotation = playerCameraRotation;
            _interactionMediator =  interactionMediator;
        }

        private void Update()
        {
            _playerMovement.Update(Time.deltaTime);
            _playerCameraRotation.Update(Time.deltaTime);
        }
    }
}