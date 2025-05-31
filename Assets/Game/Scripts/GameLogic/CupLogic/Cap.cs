using Game.Scripts.GameLogic.PlayerLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.CupLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class Cap : MonoBehaviour, ITakable
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private BoxCollider _collider;
        
        private Quaternion _initialRotation;

        public bool CanBeTaken { get; private set; } = true;
        public Rigidbody Rigidbody => _rigidbody;
        
        private void Awake()
        {
            _initialRotation = _transform.rotation;
        }

        private void OnEnable()
        {
            _collider.enabled = true;
        }

        public void Take(Transform takerParent)
        {
            _transform.SetParent(takerParent);
            _transform.localPosition = Vector3.zero;
            CanBeTaken = false;
            _transform.rotation = _initialRotation;
            _rigidbody.freezeRotation = true;
        }

        public void Drop()
        {
            _transform.SetParent(null);
            CanBeTaken = true;
            _rigidbody.freezeRotation = false;
        }

        public void DeactivateCollider()
        {
            _collider.enabled = false;
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
    }
}