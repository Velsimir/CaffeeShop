namespace Game.Scripts.GameLogic.Player
{
    public interface IPlayerMovement
    {
        public bool IsMoving { get; }
        public void Update(float deltaTime);
    }
}