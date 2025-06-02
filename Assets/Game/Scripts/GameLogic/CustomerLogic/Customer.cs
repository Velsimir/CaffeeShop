using System;
using Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.GameLogic.CustomerLogic
{
    [RequireComponent(typeof(Collider))]
    public class Customer : MonoBehaviour, ICustomer
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _toBarista;
        [SerializeField] private Transform _toExit;
        [SerializeField] private Animator _animator;
        [SerializeField] private CoffeeAcceptor _coffeeAcceptor;

        private bool _isWalking;

        public event Action<ISpawnable> Disappeared;
        
        public MonoBehaviour MonoBehaviour => this;
        public bool IsServed { get; private set; }

        private void OnEnable()
        {
            _coffeeAcceptor.GotCoffee += GoToExit;
        }

        private void OnDisable()
        {
            _coffeeAcceptor.GotCoffee -= GoToExit;
        }

        private void GoToExit()
        {
            SetDestination(_toExit.position);
            IsServed = true;
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

        public void SetDestinations(Transform baristaTable, Transform exit)
        {
            _toBarista = baristaTable;
            _toExit = exit;
            
            SetDestination(_toBarista.position);
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
            IsServed = false;
            Disappeared?.Invoke(this);
        }
    }
}