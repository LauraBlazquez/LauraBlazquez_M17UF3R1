using System.Collections.Generic;
using UnityEngine;

public class GenericPool : MonoBehaviour
{
    public static Dictionary<string, GenericPool> Pools = new Dictionary<string, GenericPool>();
    [HideInInspector] public GenericPoolSO poolConfig;
    private Queue<GameObject> bulletQueue = new Queue<GameObject>();

    public void InitializePool()
    {
        if (poolConfig == null)
        {
            Debug.LogError("No PoolConfig assigned!");
            return;
        }

        if (!Pools.ContainsKey(poolConfig.poolID))
            Pools[poolConfig.poolID] = this;
        else
            Pools[poolConfig.poolID] = this;

        bulletQueue.Clear();

        for (int i = 0; i < poolConfig.poolSize; i++)
        {
            GameObject bullet = Instantiate(poolConfig.bulletPrefab, transform);
            bullet.SetActive(false);
            bulletQueue.Enqueue(bullet);
        }

        Debug.Log($"Pool '{poolConfig.poolID}' inicializada con {poolConfig.poolSize} objetos.");
    }

    public static GenericPool GetPool(string id)
    {
        if (Pools.TryGetValue(id, out var pool))
            return pool;
        return null;
    }

    public GameObject GetBullet()
    {
        if (bulletQueue.Count > 0)
        {
            var bullet = bulletQueue.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(poolConfig.bulletPrefab, transform);
            return bullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletQueue.Enqueue(bullet);
    }
}
