using System;
using UnityEngine;

namespace Game.Scripts.GameLogic.CameraLogic.NoiseLogic
{
    public abstract class CameraNoiseSettings : ScriptableObject
    {
        public abstract ICameraNoise CreateInstance(Func<bool> shouldPlay);
    }
}