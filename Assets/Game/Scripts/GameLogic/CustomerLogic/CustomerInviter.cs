using System.Collections;
using Game.Scripts.ExtensionMethods;
using Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic;
using UnityEngine;
using Zenject;

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

        private readonly int _maxCustomers = 2;

        private int _countCustomers;
        private ISpawnerService<ICustomer> _spawnerService;
        private CutsceneManager _cutsceneManager;

        [Inject]
        private void Construct(CutsceneManager cutsceneManager)
        {
            _cutsceneManager = cutsceneManager;
        }

        private void Awake()
        {
            _spawnerService = new SpawnerService<ICustomer>((ICustomer)_customer);
            StartCoroutine(WaitDelay());
        }

        private void InviteNewCustomer()
        {
            MakeSound();

            if (_countCustomers < _maxCustomers)
            {
                ICustomer customer = _spawnerService.Spawn(transform);
                customer.SetDestinations(_baristaTable, _exit);
                customer.Disappeared += SpawnWithDelay;
                customer.ReachedCoffeePoint += ShowCutScene;
                _countCustomers++;
            }
        }

        private void ShowCutScene(ICustomer obj)
        {
            obj.ReachedCoffeePoint -= ShowCutScene;
            _cutsceneManager.StartCutscene(Cutscenes.Customer);
        }

        private void SpawnWithDelay(ISpawnable spawnable)
        {
            MakeSound();
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