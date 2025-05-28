using Game.Scripts.GameLogic.PlayerLogic;

namespace Game.Scripts.GameLogic.CupLogic
{
    public interface IFocusable
    {
        public void ActivateFocuse();
        public void DeactivateFocuse();
        public void AcceptVisitor(IFocusableVisitor focusableVisitor);
    }
}