using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    public bool shouldExpand = true;

    public static ObjectPooler instance;
    private void Awake() {
        instance = this;
    }


    void Start () {

        CreatePooledObjects();
    }

    public void CreatePooledObjects() {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++) {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject() {
        for (int i = 0; i < pooledObjects.Count; i++) {
            if (!pooledObjects[i].activeInHierarchy) {
                return pooledObjects[i];
            }
        }
        if (shouldExpand) {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            return obj;
        } else {
            return null;
        }
    }
}
