using UnityEngine;

namespace Game.Scripts.GameLogic.CupLogic
{
    public interface ITakable : IInteractable
    {
        public bool CanBeTaken { get; }
        public void Take(Transform taker);
        public void Drop();
    }
}