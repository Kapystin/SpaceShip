using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceShip
{
    public class ObjectPoolController : MasterSingleton<ObjectPoolController>
    {
        [SerializeField]
        protected EnemyController _enemyItem;
        [SerializeField]
        protected AsteroidController _asteroidItem;
        [SerializeField]
        protected PlayerItem _playerItem;
        [SerializeField]
        protected GameObject _playerBulletPrefab;
        [SerializeField]
        protected GameObject _enemyBulletPrefab;
        [SerializeField]
        protected GameObject _asteroidBrakeEffect;

        public static Dictionary<string, Queue<GameObject>> PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        private List<Pool> _objectsPool;
        private Level _levelConfig;


        private void Start()
        {
            _levelConfig = GameState.LevelConfig.SelectedLevelSettings;
            _objectsPool = new List<Pool>();

            Initialized();

            foreach (var objectItem in _objectsPool)
            {
                CreatePool(objectItem, objectItem.Tag);
            }
        }

        public void CreatePool(Pool pool, string namePoolHolder)
        {
            GameObject poolHolder = new GameObject(namePoolHolder);

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.PoolSize; i++)
            {
                GameObject newObject = Instantiate(pool.ObjectPrefab);
                newObject.SetActive(false);
                newObject.transform.SetParent(poolHolder.transform);
                objectPool.Enqueue(newObject);
            }
            PoolDictionary.Add(pool.Tag, objectPool);
        }


        public GameObject SpawnFromPool(string tagName, Vector3 position, Quaternion rotation)
        {
            if (!PoolDictionary.ContainsKey(tagName))
            {
                Debug.LogWarning("Pool with tag: " + tagName + " doesn't exist");
                return null;
            }

            GameObject objectToSpawn = PoolDictionary[tagName].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            PoolDictionary[tagName].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        private void Initialized()
        {
            AddObjectToObjectPool("Asteroid", _asteroidItem.gameObject, 30);
            AddObjectToObjectPool("Player", _playerItem.gameObject, 1);
            AddObjectToObjectPool("Enemy", _enemyItem.gameObject, 1);
            AddObjectToObjectPool("PlayerBullet", _playerBulletPrefab, 30);
            AddObjectToObjectPool("EnemyBullet", _enemyBulletPrefab, 30);
            AddObjectToObjectPool("AsteroidBreakEffect", _asteroidBrakeEffect, 7);
        }

        private void AddObjectToObjectPool(string tagName, GameObject objectPrefab, int poolSize)
        {
            Pool pool = new Pool
            {
                Tag = tagName,
                ObjectPrefab = objectPrefab,
                PoolSize = poolSize
            };
            _objectsPool.Add(pool);
        }

        private void OnEnable()
        {
            PoolDictionary.Clear();
        }

    }
}