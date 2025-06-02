using Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic;
using UnityEngine;

namespace Game.Scripts.GameLogic.CustomerLogic
{
    public interface ICustomer : ISpawnable
    {
        public bool IsServed { get; }
        public void SetDestinations(Transform baristaTable, Transform exit);
    }
}