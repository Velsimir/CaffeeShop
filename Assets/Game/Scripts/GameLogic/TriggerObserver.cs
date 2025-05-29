using System;
using UnityEngine;

namespace Game.Scripts.GameLogic
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collision> CollusionEntered;
        public event Action<Collision> CollusionExited;
        public event Action<Collider> TriggerEntered;
        public event Action<Collider> TriggerExited;

        private void OnCollisionEnter(Collision collision)
        {
            CollusionEntered?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            CollusionExited?.Invoke(collision);
        }

        private void OnTriggerEnter(Collider other)
        {
            TriggerEntered?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExited?.Invoke(other);
        }
    }
}