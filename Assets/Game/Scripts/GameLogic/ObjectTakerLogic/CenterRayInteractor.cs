using System;
using Game.Scripts.GameLogic.CupLogic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameLogic.PlayerLogic
{
    public class CenterRayInteractor : MonoBehaviour
    {
        [SerializeField] private float _rayDistance;
        [SerializeField] private LayerMask _takableLayer;

        private Camera _mainCamera;
        private IInteractable _currentInteractable;
        
        public event Action<IInteractable> InteractableDetected;
        public event Action InteractableUndetected;

        [Inject]
        private void Construct(Camera camera)
        {
            _mainCamera = camera;
        }

        private void Update()
        {
            DetectInteractable();
        }

        private void DetectInteractable()
        {
            Ray centerRay = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(_mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out RaycastHit hit, _rayDistance, _takableLayer))
            {
                IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                
                if (interactable == _currentInteractable)
                    return;
                
                InteractableDetected?.Invoke(interactable);
                _currentInteractable = interactable;
            }
            else
            {
                if (_currentInteractable == null)
                {
                    return;
                }

                InteractableUndetected?.Invoke();
                _currentInteractable = null;
            }
        }
    }
}