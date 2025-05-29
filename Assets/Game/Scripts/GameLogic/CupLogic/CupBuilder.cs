using Game.Scripts.GameLogic.PlayerLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.CupLogic
{
    public class CupBuilder : MonoBehaviour
    {
        [SerializeField] private PaperCup _paperCup;
        [SerializeField] private Transform _capHolder;
        [SerializeField] private TriggerObserver _capTriggerObserver;
        [SerializeField] private Transform _coffee;
        
        private bool _isFilled = false;
        public bool HasCap { get; private set; } = false;
        public bool IsCoffeeReady => _isFilled && HasCap;
        public PaperCup PaperCup => _paperCup;

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
            if (HasCap == false && _isFilled == true && collider.TryGetComponent(out Cap cap))
            {
                ObjectTaker objectTaker = cap.GetComponentInParent<ObjectTaker>();
                
                if (objectTaker != null)
                {
                    objectTaker.Drop();
                    cap.Take(_capHolder);
                    cap.Rigidbody.isKinematic = true;
                    HasCap = true;
                }
            }
        }

        public void FillCup()
        {
            _isFilled = true;
            _coffee.gameObject.SetActive(true);
            _paperCup.Drop();
        }
    }
}