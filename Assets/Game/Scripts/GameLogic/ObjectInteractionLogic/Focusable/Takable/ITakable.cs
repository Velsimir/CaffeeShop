using Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Takable
{
    public interface ITakable : IFocusable, ISpawnable
    {
        public bool CanBeTaken { get; }
        public Rigidbody Rigidbody { get; }
        public void Take(Transform takerParent);
        public void Drop();
    }
}