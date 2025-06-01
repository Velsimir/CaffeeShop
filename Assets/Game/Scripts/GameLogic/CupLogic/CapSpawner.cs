using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Takable;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Usable;
using Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameLogic.CupLogic
{
    public class CapSpawner : MonoBehaviour, IUsable
    {
        [SerializeField] private Cap _spawnableObject;

        private ISpawnerService<Cap> _spawnerService;
        private ITaker _objectTaker;
        
        public bool CanBeUse { get; }

        [Inject]
        private void Construct(ITaker objectTaker)
        {
            _objectTaker =  objectTaker;
        }

        private void Awake()
        {
            _spawnerService = new SpawnerService<Cap>(_spawnableObject);
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