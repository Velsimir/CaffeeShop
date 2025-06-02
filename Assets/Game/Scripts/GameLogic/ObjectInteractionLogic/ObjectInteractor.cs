using System;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Takable;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Usable;
using Game.Scripts.Infrastructure.Input;

namespace Game.Scripts.GameLogic.ObjectInteractionLogic
{
    public class ObjectInteractor : IObjectInteractor, IFocusableVisitor, IDisposable
    {
        private readonly IInputHandler _inputHandler;
        private readonly ITaker _objectTaker;

        public ObjectInteractor(IInputHandler inputHandler, ITaker objectTaker)
        {
            _inputHandler = inputHandler;
            _objectTaker = objectTaker;
            _inputHandler.InteractionButtonReleased += TryInteract;
        }

        public IFocusable CurrentFocusable { get; private set; }

        public void Get(IFocusable focusable)
        {
            focusable.ActivateFocuse();
            CurrentFocusable = focusable;
        }

        public void Remove(IFocusable focusable)
        {
            focusable.DeactivateFocuse();
            CurrentFocusable = null;
        }

        public void Visit(ITakable takable)
        {
            if (_objectTaker.IsHolding)
            {
                _objectTaker.Throw();
            }
            else
            {
                _objectTaker.TryTake(takable);
            }
        }

        public void Visit(IUsable usable)
        {
            usable.TryUse();
        }

        public void Dispose()
        {
            _inputHandler.InteractionButtonReleased -= TryInteract;
        }

        private void TryInteract()
        {
            if (_objectTaker.IsHolding && CurrentFocusable == null)
            {
                _objectTaker.Throw();
                return;
            }

            CurrentFocusable?.AcceptVisitor(this);
        }
    }
}