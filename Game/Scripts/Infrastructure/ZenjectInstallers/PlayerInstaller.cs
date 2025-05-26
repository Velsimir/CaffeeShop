using Game.Scripts.GameLogic.CameraLogic;
using Game.Scripts.GameLogic.Player;
using Game.Scripts.GameLogic.Player.GameData.Player;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.ZenjectInstallers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private CharacterController _playerCharacterController;
        [SerializeField] private PlayerCharacteristicData _playerCharacteristicData;
        [SerializeField] private CinemachineCamera _playerCamera;
        
        public override void InstallBindings()
        {
            Container.Bind<CharacterController>().FromInstance(_playerCharacterController);
            Container.Bind<IInputHandler>().To<InputHandler>().AsSingle();
            Container.Bind<IPlayerMovement>().To<PlayerMovement>().AsSingle();
            Container.Bind<PlayerCameraRotation>().To<PlayerCameraRotation>().AsSingle();
            Container.Bind<PlayerCharacteristicData>().FromInstance(_playerCharacteristicData);
            Container.Bind<CinemachineCamera>().FromInstance(_playerCamera);
        }
    }
}