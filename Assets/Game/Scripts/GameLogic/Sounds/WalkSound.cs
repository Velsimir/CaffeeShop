using Game.Scripts.GameLogic.CameraLogic;
using Game.Scripts.GameLogic.CameraLogic.Noises;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    [SerializeField] private CameraNoiseController _cameraNoiseController;
    [SerializeField] private AudioSource _audioSource;

    private WalkNoise _walkNoise;

    private void Awake()
    {
        _walkNoise = _cameraNoiseController.FindNoiseOfType<WalkNoise>();
    }

    private void OnEnable()
    {
        _walkNoise.Stepped += TurnOnStepSound;
    }
    
    private void OnDisable()
    {
        _walkNoise.Stepped -= TurnOnStepSound;
    }

    private void TurnOnStepSound()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.Play();
    }
}