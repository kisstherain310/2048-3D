using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    void Awake() => Instance = this;
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    // ---- Initialize Pool --------------------------------
    public void Initialize()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        for(int idx = 0; idx < pools.Count; idx++)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pools[idx].size; i++)
            {
                GameObject obj = Instantiate(pools[idx].prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pools[idx].tag, objectPool);
        }
    }
    // ---- Spawn Object in Pooling --------------------------------
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject objectToSpawn = GetPooledObject(tag);
        ResetObjectState(objectToSpawn);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.gameObject.transform.localScale = Vector3.one;
        objectToSpawn.SetActive(true);
        return objectToSpawn;
    }
    private GameObject GetPooledObject(string tag)
    {
        if (poolDictionary[tag].Count == 0)
        {
            GameObject obj = Instantiate(pools.Find(x => x.tag == tag).prefab);
            obj.SetActive(false);
            poolDictionary[tag].Enqueue(obj);
        }
        return poolDictionary[tag].Dequeue();
    }

    // ---- Return Object to Pooling --------------------------------
    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        ResetObjectState(objectToReturn);
        poolDictionary[tag].Enqueue(objectToReturn);
    }
    private void ResetObjectState(GameObject objectToReturn)
    {
        Rigidbody rb = objectToReturn.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        objectToReturn.gameObject.transform.localScale = Vector3.one;
        objectToReturn.transform.rotation = Quaternion.identity;
        objectToReturn.transform.position = Vector3.zero;
        objectToReturn.SetActive(false);
    }
}