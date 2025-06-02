using System.Collections;
using Game.Scripts.ExtensionMethods;
using Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.CustomerLogic
{
    public class CustomerInviter : MonoBehaviour
    {
        [SerializeField, RequireInterface(typeof(ICustomer))]
        private MonoBehaviour _customer;
        [SerializeField] private float _timerBetweenSpawns;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Transform _baristaTable;
        [SerializeField] private Transform _exit;
        
        private ISpawnerService<ICustomer> _spawnerService;

        private void Awake()
        {
            _spawnerService = new SpawnerService<ICustomer>((ICustomer)_customer);
            InviteNewCustomer();
        }

        private void InviteNewCustomer()
        {
            ICustomer customer = _spawnerService.Spawn(transform);
            customer.SetDestinations(_baristaTable, _exit);
            customer.Disappeared += SpawnWithDelay;
            MakeSound();
        }

        private void SpawnWithDelay(ISpawnable spawnable)
        {
            spawnable.Disappeared -= SpawnWithDelay;

            StartCoroutine(WaitDelay());
        }

        private IEnumerator WaitDelay()
        {
            yield return new WaitForSeconds(_timerBetweenSpawns);
            InviteNewCustomer();
        }

        private void MakeSound()
        {
            _audioSource.Play();
        }
    }
}