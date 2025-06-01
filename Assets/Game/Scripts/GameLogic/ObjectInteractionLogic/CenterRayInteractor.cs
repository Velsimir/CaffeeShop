using System;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameLogic.ObjectInteractionLogic
{
    public class CenterRayInteractor : MonoBehaviour
    {
        [SerializeField] private float _rayDistance;
        [SerializeField] private LayerMask _detectableLayer;

        private readonly Vector3 _screenCenter = new Vector3(0.5f, 0.5f, 0);
        
        private Camera _mainCamera;
        private IFocusable _currentFocusable;
        
        public event Action<IFocusable> InteractableDetected;
        public event Action<IFocusable> InteractableUndetected;

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
            if (Physics.Raycast(_mainCamera.ViewportPointToRay(_screenCenter), out RaycastHit hit, _rayDistance, _detectableLayer))
            {
                IFocusable focusable = hit.collider.GetComponent<IFocusable>();

                if (focusable == _currentFocusable)
                    return;

                if (_currentFocusable != null)
                    FocusLost();

                OnFocusDetected(focusable);
            }
            else
            {
                if (_currentFocusable != null)
                    FocusLost();
            }
        }

        private void OnFocusDetected(IFocusable focusable)
        {
            InteractableDetected?.Invoke(focusable);
            _currentFocusable = focusable;
        }

        private void FocusLost()
        {
            InteractableUndetected?.Invoke(_currentFocusable);
            _currentFocusable = null;
        }
    }
}