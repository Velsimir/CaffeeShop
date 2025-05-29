using Game.Scripts.GameLogic.PlayerLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.CupLogic
{
    public class CupBuilder : MonoBehaviour
    {
        [SerializeField] private Transform _capHolder;
        [SerializeField] private TriggerObserver _capTriggerObserver;
        
        private bool _isFilled  = true;
        private bool _hasCap  = false;
        
        public bool IsCoffeeReady => _isFilled && _hasCap;

        private void OnEnable()
        {
            _capTriggerObserver.TriggerEntered += CatchCap;
        }
        
        private void OnDisable()
        {
            _capTriggerObserver.TriggerEntered -= CatchCap;
        }

        private void CatchCap(Collider collider)
        {
            if (_hasCap == false && collider.TryGetComponent(out Cap cap))
            {
                ObjectTaker objectTaker = cap.GetComponentInParent<ObjectTaker>();
                
                if (objectTaker != null)
                {
                    objectTaker.Drop();
                    cap.Take(_capHolder);
                    cap.Rigidbody.isKinematic = true;
                    _hasCap = true;
                }
            }
        }

        public void FillCup()
        {
            _isFilled = true;
        }
    }
}