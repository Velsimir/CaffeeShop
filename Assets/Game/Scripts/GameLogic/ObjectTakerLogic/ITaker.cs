using Game.Scripts.GameLogic.CupLogic;

namespace Game.Scripts.GameLogic.PlayerLogic
{
    public interface ITaker
    {
        public ITakable CurrentItakable { get; }
        public bool IsHolding { get; }
        public void TryTake(ITakable takable);
        public void Drop();
    }
}