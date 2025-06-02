using System;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Takable;
using Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.CupLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class Cap : MonoBehaviour, ITakable, ISpawnable
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private BoxCollider _collider;
        
        private Quaternion _initialRotation;

        public event Action<ISpawnable> Disappeared;
        
        public bool CanBeTaken { get; private set; } = true;
        public Rigidbody Rigidbody => _rigidbody;
        public MonoBehaviour MonoBehaviour => this;


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
            if (_transform == null)
            {
                return;
            }
            
            _transform.SetParent(null);
            CanBeTaken = true;
            _rigidbody.freezeRotation = false;
            _rigidbody.isKinematic = false;
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

        public void Disappear()
        {
            Drop();
            gameObject.SetActive(false);
            Disappeared?.Invoke(this);
        }
    }
}