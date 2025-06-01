namespace Game.Scripts.GameLogic.PlayerLogic
{
    public interface IPlayerMovement
    {
        public bool IsMoving { get; }
        public void Update(float deltaTime);
    }
}