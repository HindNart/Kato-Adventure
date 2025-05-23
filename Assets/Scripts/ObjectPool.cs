using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject bulletPrefabs;
    public int sizePool = 10;
    private Queue<GameObject> pool;

    void Awake()
    {
        pool = new Queue<GameObject>();
        for (int i = 0; i < sizePool; i++)
        {
            GameObject obj = Instantiate(bulletPrefabs);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(bulletPrefabs);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj, float delay = 0f)
    {
        StartCoroutine(ReturnObjectAfterDelay(obj, delay));
    }

    private IEnumerator ReturnObjectAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
