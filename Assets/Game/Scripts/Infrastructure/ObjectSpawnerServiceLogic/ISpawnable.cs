using System;

namespace Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic
{
    public interface ISpawnable
    {
        public event Action<ISpawnable> Disappeared;
        public void Disappear();
    }
}