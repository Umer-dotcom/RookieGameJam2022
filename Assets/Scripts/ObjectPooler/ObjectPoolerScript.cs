using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OPTag
{
    BULLET,
    KID,
    KID2
}
public class ObjectPoolerScript : MonoBehaviour
{
    
    [System.Serializable]
    public class Pool
    {
        public OPTag tag;
        public GameObject prefab;
        public int size;
    }

    
    #region Singleton
    public static ObjectPoolerScript Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion


    public List<Pool> poolList;
    public Dictionary<OPTag, Queue<GameObject>> poolDictionary;


    
    void Start()
    {
        poolDictionary = new Dictionary<OPTag, Queue<GameObject>>();
        foreach (Pool pool in poolList)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool (OPTag tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("A pool with the tag" + tag + " has not been defiend.");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;

    }

}