using System;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Takable;
using Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.CupLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class PaperCup : MonoBehaviour, ITakable
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody _rigidbody;
        
        private Quaternion _initialRotation;

        public event Action<ISpawnable> Disappeared;
        
        public bool CanBeTaken { get; private set; } = true;
        public Rigidbody Rigidbody => _rigidbody;
        public MonoBehaviour MonoBehaviour => this;


        private void Awake()
        {
            _initialRotation = _transform.rotation;
        }

        private void OnCollisionEnter(Collision other)
        {
            
        }

        public void Take(Transform takerParent)
        {
            _transform.SetParent(takerParent);
            _transform.position = takerParent.position;
            CanBeTaken = false;
            _transform.localRotation = _initialRotation;
            _rigidbody.freezeRotation = true;
        }

        public void Drop()
        {
            _transform.SetParent(null);
            CanBeTaken = true;
            _rigidbody.freezeRotation = false;
            _rigidbody.isKinematic = false;
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