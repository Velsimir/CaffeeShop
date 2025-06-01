namespace Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Usable
{
    public interface IUsable : IFocusable
    {
        public bool CanBeUse { get; }
        public void TryUse();
    }
}