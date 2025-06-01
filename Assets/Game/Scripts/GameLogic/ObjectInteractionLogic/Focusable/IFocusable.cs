namespace Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable
{
    public interface IFocusable
    {
        public void ActivateFocuse();
        public void DeactivateFocuse();
        public void AcceptVisitor(IFocusableVisitor focusableVisitor);
    }
}