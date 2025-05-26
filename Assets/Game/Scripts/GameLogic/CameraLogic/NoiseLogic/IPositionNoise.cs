using UnityEngine;

namespace Game.Scripts.GameLogic.CameraLogic
{
    public interface IPositionNoise
    {
        public Vector3 Offset { get; }
    }
}