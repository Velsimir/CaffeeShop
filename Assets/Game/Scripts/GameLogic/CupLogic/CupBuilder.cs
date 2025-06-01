using System;
using Game.Scripts.GameLogic.ObjectInteractionLogic;
using Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.CupLogic
{
    public class CupBuilder : MonoBehaviour
    {
        [SerializeField] private PaperCup _paperCup;
        [SerializeField] private Transform _capHolder;
        [SerializeField] private TriggerObserver _capTriggerObserver;
        [SerializeField] private Transform _coffee;
        [SerializeField] private AudioSource _audioSource;
        
        private bool _isFilled = false;
        private Cap _cap;
        
        public bool HasCap { get; private set; } = false;
        public bool IsCoffeeReady => _isFilled && HasCap;
        public PaperCup PaperCup => _paperCup;


        private void OnEnable()
        {
            _capTriggerObserver.TriggerEntered += CatchCap;
        }

        private void OnDisable()
        {
            _cap?.Disappear();
            gameObject.SetActive(false);
            _capTriggerObserver.TriggerEntered -= CatchCap;
        }

        private void CatchCap(Collider collider)
        {
            if (HasCap == false && _isFilled == true && collider.TryGetComponent(out Cap cap))
            {
                ObjectTaker objectTaker = cap.GetComponentInParent<ObjectTaker>();
                
                if (objectTaker != null)
                {
                    _cap = cap;
                    _audioSource.Play();
                    objectTaker.Drop();
                    _cap.Take(_capHolder);
                    _cap.Rigidbody.isKinematic = true;
                    HasCap = true;
                    _cap.DeactivateCollider();
                    _paperCup.Drop();
                }
            }
        }

        public void FillCup()
        {
            _isFilled = true;
            _coffee.gameObject.SetActive(true);
        }

        public void DeactivateCoffee()
        {
            _isFilled = false;
            HasCap = false;
            _cap.Disappear();
            _paperCup.Disappear();
            _coffee.gameObject.SetActive(false);
        }
    }
}