using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    // Lets make and isntance of this pooler to be used as singleton
    public static EnemyPooler Instance;

    // We have to take list of Elements/Objects from the user
    // Will have a tag, prefab and the size to be spawned
    // We will make a class for that and will then make list of these objects

    [System.Serializable]
    public class Pools
    {
        public string Tag;
        public GameObject Prefab;
        public int PoolSize;
    }

    // Let's make List of these Pools
    public List<Pools> EnemyPools;

    // Let's make a dictionary of having key (string/tag), value (queue of prefabs)
    
    public Dictionary<string, Queue<GameObject>> DictPool;

    #region Singleton

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    #endregion


    // This method is just used to add the take pools from the inspector, then seeing the
    // pools size, instantiating them to that amount and then adding in the queue, and finally
    // populating our Dictionary with that data.
    private void Start()
    {
        DictPool = new Dictionary<string, Queue<GameObject>>();

        // Let's retrieve pools from inspector and insert them into queues
        foreach (var pool in EnemyPools)
        {
            // make a queue
            Queue<GameObject> enemyQueue = new Queue<GameObject>();
            for (int i = 0; i < pool.PoolSize; i++)
            {
                GameObject obj = Instantiate(pool.Prefab);
                obj.SetActive(false);
                enemyQueue.Enqueue(obj);
            }

            DictPool.Add(pool.Tag, enemyQueue);
        }
    }
    // this method will be used, where we recycle enemies from this pool and show it to them. Instantiating is just done at first and then their
    // activity is set to false, whenever we need them we active them, we can also resize the pool upon our choice
    public GameObject SpawnFromEnemyPool(string enemyTag)
    {
        if (!DictPool.ContainsKey(enemyTag))
        {
            Debug.LogWarning($"Enemy with tag: {enemyTag} does not exit!");
            return null;
        }
        // Let's take enemy from our dic and dequeue it from dict
        GameObject enemyToSpawn = DictPool[enemyTag].Dequeue();
        
        enemyToSpawn.SetActive(true);
        
        /*enemyToSpawn.transform.position = position; // we will set these as Vector3.Zero because we have already told at which direction to put our game object randomly in the game controller script
        enemyToSpawn.transform.rotation = rotation;*/

        // We also need to enqueue this GO back to dict so that dict does not get empty eventually
        
        DictPool[enemyTag].Enqueue(enemyToSpawn);

        return enemyToSpawn; // let's catch this inside our EnemySpawner
    }
}