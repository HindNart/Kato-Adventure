using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
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

    public GameObject GetObject(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        if (poolDictionary[tag].Count > 0)
        {
            GameObject obj = poolDictionary[tag].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // Find the pool with this tag
            Pool pool = pools.Find(p => p.tag == tag);
            if (pool != null)
            {
                GameObject obj = Instantiate(pool.prefab);
                return obj;
            }
            return null;
        }
    }

    public void ReturnObject(GameObject obj, string tag, float delay = 1f)
    {
        StartCoroutine(ReturnObjectAfterDelay(obj, tag, delay));
    }

    private IEnumerator ReturnObjectAfterDelay(GameObject obj, string tag, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        if (poolDictionary.ContainsKey(tag))
        {
            poolDictionary[tag].Enqueue(obj);
        }
    }
}
