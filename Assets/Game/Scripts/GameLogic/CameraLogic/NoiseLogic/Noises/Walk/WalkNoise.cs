using System;
using UnityEngine;

namespace Game.Scripts.GameLogic.CameraLogic.Noises
{
    public class WalkNoise : ICameraNoise, IPositionNoise
    {
        private readonly WalkNoiseSettings _settings;
        private readonly Func<bool> _shouldPLay;

        private float _timer;
        private Vector3 _offset;
        private Vector3 _offsetVelocity;
        private float _vertical;
        private bool _lastStepState;
        private bool _stepNow;

        public event Action Stepped;
        
        public bool IsActive { get; private set; }
        public Vector3 Offset => _offset;

        public WalkNoise(WalkNoiseSettings settings, Func<bool> shouldPLay)
        {
            _settings = settings;
            _shouldPLay = shouldPLay;
        }

        public void UpdateNoise(float deltaTime)
        {
            if (_shouldPLay())
            {
                _timer += deltaTime * _settings.Frequency;
                _vertical = Mathf.Sin(_timer) * _settings.Amplitude;
                _offset = new Vector3(0f, _vertical, 0f);
                
                SendMessageForLowestPosition();
            }
            else
            {
                _offset = Vector3.SmoothDamp(
                    _offset, Vector3.zero, ref _offsetVelocity, _settings.TimeToReturnToStartPosition, Mathf.Infinity, deltaTime);
            }
        }

        public void Enable()
        {
            IsActive = true;
        }

        public void Disable()
        {
            IsActive = false;
        }

        private void SendMessageForLowestPosition()
        {
            _stepNow = _vertical > 0;
                
            if (_stepNow && _lastStepState == false)
            {
                Stepped?.Invoke();
            }
                
            _lastStepState = _stepNow;
        }
    }
}