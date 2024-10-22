using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject prefab;
    public int initialPooler = 10;
    public int expandSize = 5;
    [SerializeField] private Queue<GameObject> gameObjectPooler;

    protected virtual void Awake()
    {
        gameObjectPooler = new Queue<GameObject>();
        for (int i = 0; i < initialPooler; i++)
        {
            CreateNewPooledObject();
        }
    }

    public GameObject GetPoolObject()
    {
        if (gameObjectPooler.Count > 0)
        {
            GameObject poolObject = gameObjectPooler.Dequeue();
            //poolObject.SetActive(true);
            return poolObject;
        }
        else
        {
            for (int i = 0; i < expandSize; i++)
            {
                CreateNewPooledObject();
            }

            return GetPoolObject();
        }
    }

    private void CreateNewPooledObject()
    { 
        GameObject pooledObject = Instantiate(prefab);
        pooledObject.SetActive(false);
        gameObjectPooler.Enqueue(pooledObject);
    }

    public void ReturnPooledObject(GameObject usedObject)
    {
        usedObject.SetActive(false);
        gameObjectPooler.Enqueue(usedObject);
    }

}
