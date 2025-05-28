namespace Game.Scripts.GameLogic.PlayerLogic
{
    public interface IUsable
    {
        public bool CanBeUse { get; }
        public void TryUse();
    }
}