using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Takable;
using Game.Scripts.Infrastructure.Input;
using Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameLogic.ObjectInteractionLogic
{
    public class ObjectTaker : MonoBehaviour, ITaker
    {
        [SerializeField] private Transform _takePlace;
        [SerializeField] private float _followForce = 500f;
        [SerializeField] private AudioSource _audioSource;

        private IInputHandler _inputHandler;
        private Camera _camera;
        
        public ITakable CurrentTakable { get; private set; }
        public Transform HoldPoint => _takePlace;
        public bool IsHolding { get; private set; }

        [Inject]
        private void Construct(IInputHandler inputHandler, Camera camera)
        {
            _inputHandler =  inputHandler;
            _camera = camera;
        }

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

        public void Throw()
        {
            if (CurrentTakable == null)
            {
                return;
            }

            ITakable takable = CurrentTakable;
            Drop();
            takable.Rigidbody.AddForce(_camera.transform.forward * 10f, ForceMode.Impulse);
        }
    }
}