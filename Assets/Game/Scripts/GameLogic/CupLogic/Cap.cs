using Game.Scripts.GameLogic.PlayerLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.CupLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class Cap : MonoBehaviour, ITakable
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
            _transform.SetParent(takerParent);
            CanBeTaken = false;
            _transform.localRotation = _initialRotation;
            _rigidbody.freezeRotation = true;
        }

        public void Drop()
        {
            _transform.SetParent(null);
            CanBeTaken = true;
            _rigidbody.freezeRotation = false;
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