using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Scripts.Infrastructure.ObjectSpawnerServiceLogic
{
    public class SpawnerService<TSpawnableObjet> : ISpawnerService<TSpawnableObjet>
        where TSpawnableObjet : ISpawnable
    {
        private readonly ObjectPoolService<TSpawnableObjet> _pool;

        public SpawnerService(TSpawnableObjet prefab, Transform container = null, DiContainer diContainer = null)
        {
            _pool = new ObjectPoolService<TSpawnableObjet>(prefab, container, diContainer);
        }

        public TSpawnableObjet Spawn(Transform spawnPoint)
        {
            return Spawn(spawnPoint.position, spawnPoint.rotation);
        }

        public TSpawnableObjet Spawn(Vector3 position, Quaternion rotation)
        {
            TSpawnableObjet instance = _pool.Get(position);
            instance.MonoBehaviour.transform.SetPositionAndRotation(position, rotation);
            return instance;
        }

        public TInterface SpawnAs<TInterface>(Vector3 position, Quaternion rotation)
            where TInterface : class, ISpawnable
        {
            var instance = Spawn(position, rotation);
            return instance as TInterface;
        }
        
        public TInterface SpawnAs<TInterface>(Transform spawnPoint)
            where TInterface : class, ISpawnable
        {
            return SpawnAs<TInterface>(spawnPoint.position, spawnPoint.rotation);
        }


        private class ObjectPoolService<TSpawnable> where TSpawnable : ISpawnable
        {
            private readonly TSpawnable _prefab;
            private readonly Transform _container;
            private DiContainer _diContainer;
            private readonly List<TSpawnable> _available = new();

            public ObjectPoolService(TSpawnable prefab, Transform container = null, DiContainer diContainer = null)
            {
                _prefab = prefab;
                _container = container;
                _diContainer = diContainer;
            }

            public TSpawnable Get(Vector3 position)
            {
                TSpawnable instance;

                if (_available.Count > 0)
                {
                    instance = _available[0];
                    _available.RemoveAt(0);
                }
                else
                {
                    instance = InstantiateFromPrefab(position);
                    instance.Disappeared += ReturnToPool;
                }

                instance.MonoBehaviour.gameObject.SetActive(true);
                return instance;
            }

            private void ReturnToPool(ISpawnable spawnable)
            {
                if (spawnable is TSpawnable t)
                {
                    t.MonoBehaviour.gameObject.SetActive(false);
                    _available.Add(t);
                }
            }
            
            private TSpawnable InstantiateFromPrefab(Vector3 position)
            {
                GameObject obj;
                
                if (_diContainer != null)
                {
                    obj = _diContainer.InstantiatePrefab(_prefab.MonoBehaviour.gameObject, position, _prefab.MonoBehaviour.transform.rotation, _container);
                }
                else
                {
                    obj = Object.Instantiate(_prefab.MonoBehaviour.gameObject, position, _prefab.MonoBehaviour.transform.rotation, _container);
                }

                return obj.GameObject().GetComponent<TSpawnable>();
            }
        }
    }
}