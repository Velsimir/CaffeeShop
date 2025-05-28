using Game.Scripts.GameLogic.CupLogic;

namespace Game.Scripts.GameLogic.PlayerLogic
{
    public interface IFocusableVisitor
    {
        public void Visit(ITakable takable);
        public void Visit(IUsable usable);
    }
}