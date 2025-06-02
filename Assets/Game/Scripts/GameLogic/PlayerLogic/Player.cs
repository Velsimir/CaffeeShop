using Game.Scripts.GameLogic.ObjectInteractionLogic;
using Game.Scripts.Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameLogic.PlayerLogic
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        private IPlayerMovement _playerMovement;
        private CameraRotation _cameraRotation;
        private InteractionMediator _interactionMediator;

        [Inject]
        private void Construct(IPlayerMovement playerMovement, CameraRotation cameraRotation,
            InteractionMediator interactionMediator)
        {
            _playerMovement = playerMovement;
            _cameraRotation = cameraRotation;
            _interactionMediator = interactionMediator;
        }

        private void Update()
        {
            _playerMovement.Update(Time.deltaTime);
            _cameraRotation.Update(Time.deltaTime);
        }
    }
}