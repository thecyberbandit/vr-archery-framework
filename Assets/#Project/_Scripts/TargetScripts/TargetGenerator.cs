using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour {

    public ObjectPooler objectPooler;

    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public GameObject targetPrefab;


    void Start ()
    {
        InvokeRepeating("SpawnTarget", 7f, 4f);
    }

    //public void SpawnTarget()
    //{
    //    int random = Random.Range(0, 2);

    //    GameObject newTarget = objectPooler.GetPooledObject();

    //    if (random == 0)
    //    {
    //        newTarget.transform.position = spawnPoint1.transform.position;
    //        newTarget.transform.rotation = spawnPoint1.transform.rotation;
    //    }

    //    else if (random == 1)
    //    {
    //        newTarget.transform.position = spawnPoint2.transform.position;
    //        newTarget.transform.rotation = spawnPoint2.transform.rotation;
    //    }

    //    newTarget.GetComponent<MeshRenderer>().enabled = true;
    //    GameManager.Instance.canAppear = false;
    //}

    public void SpawnTarget()
    {
        int random = Random.Range(0, 2);

        GameObject newTarget = Instantiate(targetPrefab);

        if (random == 0)
        {
            newTarget.transform.position = spawnPoint1.transform.position;
            newTarget.transform.rotation = spawnPoint1.transform.rotation;
        }

        else if (random == 1)
        {
            newTarget.transform.position = spawnPoint2.transform.position;
            newTarget.transform.rotation = spawnPoint2.transform.rotation;
        }
    }
}
