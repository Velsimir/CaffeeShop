using Game.Scripts.GameLogic.PlayerLogic;
using Game.Scripts.Infrastructure.GameData.Player;
using Game.Scripts.Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.ZenjectInstallers
{
    public class PlayerInstaller : MonoInstaller
    {
        //TODO добавить возможность загружать не из SerializeField а после загрузки из ресурсов
        [SerializeField] private CharacterController _playerCharacterController;
        [SerializeField] private PlayerCharacteristicData _playerCharacteristicData;
        
        public override void InstallBindings()
        {
            Container.Bind<CharacterController>().FromInstance(_playerCharacterController);
            Container.Bind<IInputHandler>().To<InputHandler>().AsSingle();
            Container.Bind<IPlayerMovement>().To<PlayerMovement>().AsSingle();
            Container.Bind<CameraRotation>().To<CameraRotation>().AsSingle();
            Container.Bind<PlayerCharacteristicData>().FromInstance(_playerCharacteristicData);
        }
    }
}