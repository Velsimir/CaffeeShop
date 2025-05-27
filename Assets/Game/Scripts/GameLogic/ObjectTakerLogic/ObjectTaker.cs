using Game.Scripts.GameLogic.CupLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.PlayerLogic
{
    public class ObjectTaker : ITaker
    {
        private readonly Transform _takePlace;

        public ObjectTaker(Transform takePlace)
        {
            _takePlace = takePlace;
        }

        public ITakable CurrentItakable { get; private set;}
        public bool IsHolding { get; private set; }

        public void TryTake(ITakable takable)
        {
            if (takable.CanBeTaken == false)
            {
                return;
            }
            
            takable.Take(_takePlace);
            IsHolding = true;
            CurrentItakable = takable;
        }

        public void Drop()
        {
            CurrentItakable.Drop();
            CurrentItakable = null;
            IsHolding = false;
        }
    }
}