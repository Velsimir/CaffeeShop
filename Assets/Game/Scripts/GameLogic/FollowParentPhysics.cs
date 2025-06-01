using UnityEngine;

namespace Game.Scripts.GameLogic
{
    public class FollowParentPhysics : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _followForce = 500f;

        private void FixedUpdate()
        {
            if (_rigidbody == null || transform.parent == null)
                return;

            Vector3 targetPosition = transform.parent.position;
            Vector3 direction = targetPosition - transform.position;

            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.AddForce(direction * _followForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}