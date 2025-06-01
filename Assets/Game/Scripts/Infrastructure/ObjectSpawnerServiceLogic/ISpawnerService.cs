using UnityEngine;

namespace Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic
{
    public interface ISpawnerService<TSpawnableObjet> where TSpawnableObjet : MonoBehaviour, ISpawnable
    {
        TSpawnableObjet Spawn();
        TSpawnableObjet Spawn(Transform at);
    }
}