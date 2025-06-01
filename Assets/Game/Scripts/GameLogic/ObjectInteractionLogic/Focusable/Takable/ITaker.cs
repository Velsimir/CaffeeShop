namespace Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Takable
{
    public interface ITaker
    {
        public bool IsHolding { get; }
        public void TryTake(ITakable takable);
        public void Drop();
    }
}