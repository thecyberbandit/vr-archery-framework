using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public GameObject pooledObject;

    public int pooledAmount;

    List<GameObject> pooledObjects;


    void Start()
    {
        pooledObjects = new List<GameObject>();
        AddObjects();
    }

    void AddObjects()
    {
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.transform.position = new Vector3(6.73f, 12.6f, 8.8f);
            obj.transform.rotation = Quaternion.identity;
            obj.GetComponent<MeshRenderer>().enabled = false;
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].GetComponent<MeshRenderer>().enabled)
            {
                return pooledObjects[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(pooledObject);
        obj.GetComponent<MeshRenderer>().enabled = false;
        pooledObjects.Add(obj);
        return obj;
    }
}
