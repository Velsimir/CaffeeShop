using UnityEngine;

namespace Game.Scripts.GameLogic.CupLogic
{
    public interface ITakable : IFocusable
    {
        public bool CanBeTaken { get; }
        public void Take(Transform taker);
        public void Drop();
    }
}