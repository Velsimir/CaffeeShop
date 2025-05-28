using System;
using Game.Scripts.GameLogic.CupLogic;

namespace Game.Scripts.GameLogic.PlayerLogic
{
    public class InteractionMediator : IDisposable
    {
        private readonly CenterRayInteractor _centerRayInteractor;
        private readonly IObjectInteractor _objectInteractor;
        
        public InteractionMediator(CenterRayInteractor centerRayInteractor, IObjectInteractor objectInteractor)
        {
            _centerRayInteractor = centerRayInteractor;
            _objectInteractor = objectInteractor;
            
            _centerRayInteractor.InteractableDetected += GetFocusableObject;
            _centerRayInteractor.InteractableUndetected += UnInteract;
        }

        public void Dispose()
        {
            _centerRayInteractor.InteractableDetected -= GetFocusableObject;
            _centerRayInteractor.InteractableUndetected -= UnInteract;
        }

        private void GetFocusableObject(IFocusable focusable)
        {
            _objectInteractor.Get(focusable);
        }

        private void UnInteract(IFocusable focusable)
        {
            _objectInteractor.Remove(focusable);
        }
    }
}