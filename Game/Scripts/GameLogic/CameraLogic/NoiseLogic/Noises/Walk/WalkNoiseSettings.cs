using System;
using UnityEngine;

namespace Game.Scripts.GameLogic.CameraLogic.Noises
{
    [CreateAssetMenu(fileName = "WalkNoiseSettings", menuName = "Noise Settings / WalkNoiseSettings")]
    public class WalkNoiseSettings : CameraNoiseSettings
    {
        [SerializeField] private float _amplitude = 0.05f;
        [SerializeField] private float _frequency = 6f;
        [SerializeField] private float _timeToReturnToStartPosition = 0.1f;

        public float Amplitude => _amplitude;
        public float Frequency => _frequency;
        public float TimeToReturnToStartPosition => _timeToReturnToStartPosition;

        public override ICameraNoise CreateInstance(Func<bool> shouldPlay)
        {
            return new WalkNoise(this, shouldPlay);
        }
    }
}