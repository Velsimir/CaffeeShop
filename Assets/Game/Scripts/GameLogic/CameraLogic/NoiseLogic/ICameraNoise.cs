namespace Game.Scripts.GameLogic.CameraLogic
{
    public interface ICameraNoise
    {
        public void UpdateNoise(float deltaTime);
        public void Enable();
        public void Disable();
        public bool IsActive { get; }
    }
}