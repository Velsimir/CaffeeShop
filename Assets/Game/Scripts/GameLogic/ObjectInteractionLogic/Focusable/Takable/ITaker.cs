using UnityEngine;

namespace Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Takable
{
    public interface ITaker
    {
        public Transform HoldPoint { get; }
        public bool IsHolding { get; }
        public void TryTake(ITakable takable);
        public void Drop();
        public void Throw();
    }
}