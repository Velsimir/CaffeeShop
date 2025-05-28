using Game.Scripts.GameLogic.PlayerLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.CupLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class Cup : MonoBehaviour, ITakable
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody _rigidbody;
        
        private Quaternion _initialRotation;

        public bool CanBeTaken { get; private set; } = true;

        private void Awake()
        {
            _initialRotation = _transform.rotation;
        }

        public void Take(Transform takerParent)
        {
            _rigidbody.isKinematic = true;
            _transform.SetParent(takerParent);
            _transform.localPosition = Vector3.zero;
            CanBeTaken = false;
            _transform.localRotation = _initialRotation;
        }

        public void Drop()
        {
            _rigidbody.isKinematic = false;
            _transform.SetParent(null);
            CanBeTaken = true; 
        }

        public void ActivateFocuse()
        {
            Debug.Log("Cuo OnFocused");
        }

        public void DeactivateFocuse()
        {
            Debug.Log("Cuo UnFocused");
        }

        public void AcceptVisitor(IFocusableVisitor focusableVisitor)
        {
            focusableVisitor.Visit(this);
        }
    }
}