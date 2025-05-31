using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.GameLogic.CameraLogic.Noises;
using Game.Scripts.GameLogic.Player;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameLogic.CameraLogic
{
    public class CameraNoiseController : MonoBehaviour
    {
        [SerializeField] private List<CameraNoiseSettings> _noisePresets;
        
        private readonly List<ICameraNoise> _activeNoises = new();
        
        private Vector3 _initialPosition;
        private CinemachineCamera _cameraTarget;

        public Func<bool> WalkCondition { get; set; } = () => false;
        
        [Inject]
        private void Construct(IPlayerMovement playerMovement, CinemachineCamera cinemachineCamera)
        {
            _cameraTarget = cinemachineCamera;
            _initialPosition = _cameraTarget.transform.localPosition;

            WalkCondition = () => playerMovement.IsMoving;
            
            foreach (var noiseSetting in _noisePresets)
            {
                ICameraNoise noise = noiseSetting.CreateInstance(WalkCondition);
                RegisterNoise(noise);
            }
        }

        private void Update()
        {
            Vector3 offset = Vector3.zero;

            foreach (var noise in _activeNoises)
            {
                if (noise.IsActive)
                {
                    noise.UpdateNoise(Time.deltaTime);

                    if (noise is IPositionNoise positionNoise)
                    {
                        offset += positionNoise.Offset;
                    }
                }
            }
            
            _cameraTarget.transform.localPosition = _initialPosition + offset; }

        public void RegisterNoise(ICameraNoise noise)
        {
            if (_activeNoises.Contains(noise) == true)
            {
                return;
            }
            
            _activeNoises.Add(noise);
            noise.Enable();
        }

        public void UnRegisterNoise(ICameraNoise noise)
        {
            if (_activeNoises.Contains(noise) == false)
            {
                return;
            }
            
            _activeNoises.Remove(noise);
            noise.Disable();
        }

        public ICameraNoise FindNoise<TCameraNoise>() where TCameraNoise : class, ICameraNoise
        {
            return _activeNoises.FirstOrDefault(noise => noise is TCameraNoise);
        }

        public TCameraNoise FindNoiseOfType<TCameraNoise>() where TCameraNoise : class
        {
            return _activeNoises.OfType<TCameraNoise>().FirstOrDefault();
        }
    }
}