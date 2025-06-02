using System;
using Game.Scripts.GameLogic.ObjectInteractionLogic.Focusable.Takable;
using Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.GameLogic.CustomerLogic
{
    [RequireComponent(typeof(Collider))]
    public class Customer : MonoBehaviour, ISpawnable
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _toBarista;
        [SerializeField] private Transform _toExit;
        [SerializeField] private Animator _animator;
        [SerializeField] private CoffeeAccepter _coffeeAccepter;

        private bool _isWalking;

        public event Action<ISpawnable> Disappeared;
        
        public MonoBehaviour MonoBehaviour => this;

        private void Awake()
        {
            SetDestination(_toBarista.position);
        }

        private void OnEnable()
        {
            _coffeeAccepter.GotCoffee += GoToExit;
        }

        private void OnDisable()
        {
            _coffeeAccepter.GotCoffee -= GoToExit;
        }

        private void GoToExit()
        {
            SetDestination(_toExit.position);
        }

        private void Update()
        {
            if (_isWalking && !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    ArrivedAtDestination();
                }
            }
        }

        private void ArrivedAtDestination()
        {
            _isWalking = false;
            _animator.SetBool("IsWalking", _isWalking);
        }

        private void SetDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
            _isWalking = true;
            _animator.SetBool("IsWalking", _isWalking);
        }

        public void Disappear()
        {
            Disappeared?.Invoke(this);
        }
    }
}