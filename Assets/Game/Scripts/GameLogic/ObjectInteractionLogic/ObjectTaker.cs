using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Takable;
using Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.ObjectInteractionLogic
{
    public class ObjectTaker : MonoBehaviour, ITaker
    {
        [SerializeField] private Transform _takePlace;
        [SerializeField] private float _followForce = 500f;
        [SerializeField] private AudioSource _audioSource;
        
        public ITakable CurrentTakable { get; private set; }
        public bool IsHolding { get; private set; }

        private void FixedUpdate()
        {
            if (CurrentTakable == null)
                return;

            Vector3 targetPosition = _takePlace.position;
            Vector3 direction = targetPosition - CurrentTakable.Rigidbody.position;

            CurrentTakable.Rigidbody.linearVelocity = Vector3.zero;
            CurrentTakable.Rigidbody.AddForce(direction * _followForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        
        public void TryTake(ITakable takable)
        {
            if (IsHolding == true || takable.CanBeTaken == false)
            {
                Debug.Log("У вас уже есть объектв в руках или не может быть взят");
                return;
            }
            
            _audioSource.Play();
            takable.Take(_takePlace);
            CurrentTakable = takable;
            takable.Disappeared += UnTakeCurrentObject;
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

        private void UnTakeCurrentObject(ISpawnable obj)
        {
            obj.Disappeared -= UnTakeCurrentObject;
            CurrentTakable = null;
            IsHolding = false;
        }
    }
}