using System.Collections;
using Game.Scripts.GameLogic.CupLogic;
using Game.Scripts.GameLogic.PlayerLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic
{
    public class CoffeeMachine : MonoBehaviour, IUsable
    {
        [SerializeField] private Transform _cupHolder;
        [SerializeField] private TriggerObserver _cupTriggerObserver;
        [SerializeField] private Transform _coffeeStream;
        [SerializeField] private AudioSource _audioSource;

        private bool _hasCap = false;
        private CupBuilder _cupBuilder;
        
        public bool CanBeUse { get; private set; } = false;

        private void OnEnable()
        {
            _cupTriggerObserver.TriggerEntered += TryTakeCup;
        }

        private void OnDisable()
        {
            _cupTriggerObserver.TriggerEntered -= TryTakeCup;
        }

        private void TryTakeCup(Collider collider)
        {
            if (_hasCap)
            {
                return;
            }
            
            _cupBuilder = collider.GetComponent<CupBuilder>();
            
            if (_cupBuilder != null)
            {
                ObjectTaker objectTaker = _cupBuilder.GetComponentInParent<ObjectTaker>();
                
                if (objectTaker != null && _cupBuilder.HasCap == false)
                {
                    objectTaker.Drop();
                    _cupBuilder.PaperCup.Take(_cupHolder);
                    CanBeUse = true;
                    _hasCap = true;
                    _cupBuilder.PaperCup.Rigidbody.isKinematic = true;
                }
            }
        }

        public void TryUse()
        {
            if (CanBeUse == false)
            {
                return;
            }

            StartCoroutine(TurnOnCoffeeStream());
        }

        public void ActivateFocuse()
        {
        }

        public void DeactivateFocuse()
        {
        }

        public void AcceptVisitor(IFocusableVisitor focusableVisitor)
        {
            focusableVisitor.Visit(this);
        }

        private IEnumerator TurnOnCoffeeStream()
        {
            _audioSource.Play();
            
            yield return new WaitForSeconds(0.1f);
            
            _coffeeStream.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(5);
           
            _audioSource.Stop();
            _coffeeStream.gameObject.SetActive(false);
            _cupBuilder.FillCup();
            CanBeUse = false;
            _hasCap = false;
        }
    }
}