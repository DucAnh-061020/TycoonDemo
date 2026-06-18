using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    // Maps a specific Prefab to its own dedicated ObjectPool
    private Dictionary<int, ObjectPool<GameObject>> pools = new Dictionary<int, ObjectPool<GameObject>>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Retrieves a GameObject from the pool, or creates a new pool if one doesn't exist for this prefab.
    /// </summary>
    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, int indexKey)
    {
        if (!pools.ContainsKey(indexKey))
        {
            CreatePool(prefab, indexKey);
        }

        GameObject spawnedObject = pools[indexKey].Get();
        spawnedObject.transform.SetPositionAndRotation(position, rotation);
        return spawnedObject;
    }

    /// <summary>
    /// Returns an object back to its respective pool to be reused later.
    /// </summary>
    public void Release(GameObject instance, int indexKey)
    {
        if (pools.ContainsKey(indexKey))
        {
            pools[indexKey].Release(instance);
        }
        else
        {
            // Fallback just in case something tries to release an unpooled object
            Destroy(instance);
        }
    }

    private void CreatePool(GameObject prefab, int indexKey)
    {
        // Define how the Unity ObjectPool should handle this specific prefab
        ObjectPool<GameObject> newPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(prefab), // How to make a new one if the pool is empty
            actionOnGet: (obj) => obj.SetActive(true), // What to do when taking from the pool
            actionOnRelease: (obj) => obj.SetActive(false), // What to do when returning to the pool
            actionOnDestroy: (obj) => Destroy(obj), // What to do if we exceed max pool size
            collectionCheck: false, // Set to true for debugging if objects are released twice
            defaultCapacity: 50,
            maxSize: 1000 // Prevents memory leaks if millions are spawned
        );

        pools.Add(indexKey, newPool);
    }
}