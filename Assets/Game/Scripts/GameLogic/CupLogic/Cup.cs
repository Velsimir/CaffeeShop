using System;
using UnityEngine;
using UnityEngine.UI;

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

        public void Take(Transform taker)
        {
            _rigidbody.useGravity = false;
            _transform.SetParent(taker);
            CanBeTaken = false;
            _transform.rotation = _initialRotation;
        }

        public void Drop()
        {
            _rigidbody.useGravity = true;
            _transform.SetParent(null);
            CanBeTaken = true; 
        }

        public void OnFocused()
        {
            Debug.Log("Cuo OnFocused");
        }

        public void UnFocused()
        {
            Debug.Log("Cuo UnFocused");
        }
    }
}