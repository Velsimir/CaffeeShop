using System;
using Game.Scripts.GameLogic.CupLogic;

namespace Game.Scripts.GameLogic.PlayerLogic
{
    public class ObjectInteractor : IObjectInteractor, IFocusableVisitor, IDisposable
    {
        private readonly IInputHandler _inputHandler;
        private readonly ITaker _objectTaker;
        private readonly IUser _objectUser;

        public ObjectInteractor(IInputHandler inputHandler, ITaker objectTaker, IUser objectUser)
        {
            _inputHandler = inputHandler;
            _objectTaker = objectTaker;
            _objectUser = objectUser;
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
                _objectTaker.Drop();
            }
            else
            {
                _objectTaker.TryTake(takable);
            }
        }

        public void Visit(IUsable usable)
        {
            _objectUser.Use(usable);
        }

        public void Dispose()
        {
            _inputHandler.InteractionButtonReleased -= TryInteract;
        }

        private void TryInteract()
        {
            if (_objectTaker.IsHolding && CurrentFocusable == null)
            {
                _objectTaker.Drop();
                return;
            }

            CurrentFocusable?.AcceptVisitor(this);
        }
    }
}