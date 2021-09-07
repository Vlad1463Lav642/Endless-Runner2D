using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private Pooler coinPooler;

    [SerializeField] private float distanceToCoin;

    public void SpawnCoins(Vector3 startPosition)
    {
        GameObject coin1 = coinPooler.GeneratePlatform();
        coin1.transform.position = startPosition;
        coin1.SetActive(true);

        GameObject coin2 = coinPooler.GeneratePlatform();
        coin2.transform.position = new Vector3(startPosition.x - distanceToCoin, startPosition.y, startPosition.z);
        coin2.SetActive(true);

        GameObject coin3 = coinPooler.GeneratePlatform();
        coin3.transform.position = new Vector3(startPosition.x + distanceToCoin, startPosition.y,startPosition.z);
        coin3.SetActive(true);
    }
}