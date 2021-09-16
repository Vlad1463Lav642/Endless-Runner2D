using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Обеспечивает спавн различных объектов.
/// </summary>
public class Pooler : MonoBehaviour
{
    [SerializeField] private GameObject pooledGameObject;

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

    /// <summary>
    /// Возвращает объект спавна.
    /// </summary>
    /// <returns>Объект спавна.</returns>
    public GameObject GetPooledGameObject()
    {
        return pooledGameObject;
    }
}