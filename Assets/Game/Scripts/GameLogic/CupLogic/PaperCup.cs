using System;
using Game.Scripts.GameLogic.PlayerLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.CupLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class PaperCup : MonoBehaviour, ITakable
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody _rigidbody;
        
        private Quaternion _initialRotation;

        public bool CanBeTaken { get; private set; } = true;
        public Rigidbody Rigidbody => _rigidbody;

        private void Awake()
        {
            _initialRotation = _transform.rotation;
        }

        public void Take(Transform takerParent)
        {
            _transform.SetParent(takerParent);
            CanBeTaken = false;
            _transform.position = takerParent.position;
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