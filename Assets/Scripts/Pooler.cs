using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] private GameObject pooledGameObject;
    [SerializeField] private int amount;

    private List<GameObject> pooledGameObjectsList;

    private void Start()
    {
        pooledGameObjectsList = new List<GameObject>();
    }

    public GameObject GeneratePlatform()
    {
        for(int i=0; i < pooledGameObjectsList.Count; i++)
        {
            if (!pooledGameObjectsList[i].activeInHierarchy)
            {
                return pooledGameObjectsList[i];
            }
        }

        GameObject obj = Instantiate(pooledGameObject);
        obj.SetActive(false);
        pooledGameObjectsList.Add(obj);

        return obj;
    }

    public GameObject GetPooledGameObject()
    {
        return pooledGameObject;
    }
}