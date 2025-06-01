using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Takable;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Usable;

namespace Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable
{
    public interface IFocusableVisitor
    {
        public void Visit(ITakable takable);
        public void Visit(IUsable usable);
    }
}