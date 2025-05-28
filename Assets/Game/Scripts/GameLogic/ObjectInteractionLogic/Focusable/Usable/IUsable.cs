using Game.Scripts.GameLogic.CupLogic;

namespace Game.Scripts.GameLogic.PlayerLogic
{
    public interface IUsable : IFocusable
    {
        public bool CanBeUse { get; }
        public void TryUse();
    }
}