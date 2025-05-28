using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.ZenjectInstallers
{
    public class CameraInstaller : MonoInstaller
    {
        //TODO добавить возможность загружать не из SerializeField а после загрузки из ресурсов
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private CinemachineCamera _cinemachineCamera;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_playerCamera);
            Container.Bind<CinemachineCamera>().FromInstance(_cinemachineCamera);
        }
    }
}