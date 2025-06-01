using Game.Scripts.GameLogic.ObjectInteractionLogic;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Takable;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Usable;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.ZenjectInstallers
{
    public class InteractorInstaller : MonoInstaller
    {
        //TODO добавить возможность загружать не из SerializeField а после загрузки из ресурсов
        [SerializeField] private ObjectTaker _objectTaker;
        [SerializeField] private CenterRayInteractor _centerRayInteractor;
        
        public override void InstallBindings()
        {
            Container.Bind<ITaker>().To<ObjectTaker>().FromInstance(_objectTaker);
            Container.Bind<CenterRayInteractor>().FromInstance(_centerRayInteractor);
            Container.Bind<InteractionMediator>().AsSingle();
            Container.Bind<IObjectInteractor>().To<ObjectInteractor>().AsSingle();
        }
    }
}