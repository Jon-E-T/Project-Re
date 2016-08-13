using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    // use Prefab GameObject!
    public GameObject pooledObject;
    public int pooledAmount;

    List<GameObject> pooledListObjects;

    void Start()
    {
        pooledListObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledListObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledListObjects.Count; i++)
        {
            if (!pooledListObjects[i].activeInHierarchy)
            {
                return pooledListObjects[i];
            }
        }

        GameObject obj = Instantiate(pooledObject);
        obj.SetActive(false);
        pooledListObjects.Add(obj);
        return obj;
    }
}
