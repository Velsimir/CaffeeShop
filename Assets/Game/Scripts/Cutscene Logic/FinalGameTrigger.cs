using System.Collections;
using Game.Scripts.GameLogic.CustomerLogic;
using Game.Scripts.GameLogic.PlayerLogic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Cutscene_Logic
{
    [RequireComponent(typeof(BoxCollider))]
    public class FinalGameTrigger : MonoBehaviour
    {
        [SerializeField] private CustomerInviter _customerInviter;
        [SerializeField] private AudioSource _audioSource;
        
        private CutsceneManager _cutsceneManager;
        private bool _isStarted;
        
        [Inject]
        private void Construct(CutsceneManager cutsceneManager)
        {
            _cutsceneManager = cutsceneManager;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player) && _customerInviter.IsEnded && _isStarted == false)
            {
                _isStarted =  true;
                StartCoroutine(StartFinalCutSceneWithDelay());
            }
        }

        private IEnumerator StartFinalCutSceneWithDelay()
        {
            _audioSource.Play();
            yield return new WaitForSeconds(Random.Range(16f, 20f));
            _cutsceneManager.StartCutscene(Cutscenes.Final);
        }
    }
}