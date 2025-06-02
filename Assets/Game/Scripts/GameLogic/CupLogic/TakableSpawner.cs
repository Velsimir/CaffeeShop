using Game.Scripts.ExtensionMethods;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Takable;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Usable;
using Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameLogic.CupLogic
{
    public class TakableSpawner : MonoBehaviour, IUsable
    {
        [SerializeField, RequireInterface(typeof(ITakable))]
        private MonoBehaviour _spawnableObject;

        private ISpawnerService<ITakable> _spawnerService;
        private ITaker _objectTaker;
        
        public bool CanBeUse { get; }

        [Inject]
        private void Construct(ITaker objectTaker)
        {
            _objectTaker =  objectTaker;
        }

        private void Awake()
        {
            _spawnerService = new SpawnerService<ITakable>((ITakable)_spawnableObject);
        }

        public void TryUse()
        {
            if (_objectTaker.IsHolding)
            {
                return;
            }

            _objectTaker.TryTake(_spawnerService.Spawn(transform));
        }

        public void ActivateFocuse()
        { }

        public void DeactivateFocuse()
        { }

        public void AcceptVisitor(IFocusableVisitor focusableVisitor)
        {
            focusableVisitor.Visit(this);
        }
    }
}