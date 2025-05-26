using Unity.Cinemachine;

namespace Game.Scripts.GameLogic.CameraLogic
{
    public class WalkNoise
    {
        private readonly CinemachineCamera _cinemachineCamera;

        public WalkNoise(CinemachineCamera cinemachineCamera)
        {
            _cinemachineCamera = cinemachineCamera;
        }
    }
}