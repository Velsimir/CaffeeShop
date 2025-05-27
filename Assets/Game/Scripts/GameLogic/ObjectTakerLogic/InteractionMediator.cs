using System;
using Game.Scripts.GameLogic.CupLogic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameLogic.PlayerLogic
{
    public class InteractionMediator : MonoBehaviour
    {
        [SerializeField] private CenterRayInteractor _centerRayInteractor;
        
        private ObjectTaker _objectTaker;
        private IInteractable _currentInteractable;
        private IInputHandler _inputHandler;

        [Inject]
        private void Construct(IInputHandler inputHandler)
        {
            _objectTaker = new ObjectTaker(transform);
            _inputHandler = inputHandler;
        }

        private void OnEnable()
        {
            _centerRayInteractor.InteractableDetected += GetInteractedObject;
            _centerRayInteractor.InteractableUndetected += UnInteract;
            _inputHandler.InteractionButtonReleased += TryInteract;
        }

        private void TryInteract()
        {
            if (_currentInteractable == null)
            {
                return;
            }
            
            ITakable takable = _currentInteractable as ITakable;
            _objectTaker.TryTake(takable);
        }

        private void OnDisable()
        {
            _centerRayInteractor.InteractableDetected -= GetInteractedObject;
            _centerRayInteractor.InteractableUndetected -= UnInteract;
        }

        private void GetInteractedObject(IInteractable interactable)
        {
            interactable.OnFocused();
            _currentInteractable =  interactable;
        }

        private void UnInteract()
        {
            if (_currentInteractable == null )
            {
                return;
            }
            
            _currentInteractable.UnFocused();
            _currentInteractable = null;
        }
    }
}