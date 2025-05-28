using Game.Scripts.GameLogic.CupLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.PlayerLogic
{
    public class ObjectTaker : MonoBehaviour, ITaker
    {
        [SerializeField] private Transform _takePlace;

        public ITakable CurrentTakable { get; private set; }
        public bool IsHolding { get; private set; }

        public void TryTake(ITakable takable)
        {
            if (IsHolding == true || takable.CanBeTaken == false)
            {
                Debug.Log("У вас уже есть объектв в руках или не может быть взят");
                return;
            }
            
            takable.Take(_takePlace);
            CurrentTakable = takable;
            IsHolding = true;
        }

        public void Drop()
        {
            if (IsHolding == false)
            {
                Debug.Log("У вас в руках нет предмета");
                return;
            }
            
            CurrentTakable.Drop();
            CurrentTakable = null;
            IsHolding = false;
        }
    }
}