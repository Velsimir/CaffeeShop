using System;
using UnityEngine;

namespace Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic
{
    public interface ISpawnable
    {
        public MonoBehaviour MonoBehaviour { get; }
        public event Action<ISpawnable> Disappeared;
        public void Disappear();
    }
}