using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private Pooler coinPooler;
    [SerializeField] private Pooler crownPooler;
    [SerializeField] private Pooler ringPooler;
    [SerializeField] private Pooler diamondPooler;

    [SerializeField] private float distanceToCoin;
    [SerializeField] private float distanceToCrown;
    [SerializeField] private float distanceToRing;
    [SerializeField] private float distanceToDiamond;

    public void SpawnCoins(Vector3 startPosition)
    {
        int randomNumber = Mathf.RoundToInt(Random.Range(0f, 3f));

        switch (randomNumber)
        {
            case 0:
                GameObject coin1 = coinPooler.GeneratePlatform();
                coin1.transform.position = startPosition;
                coin1.SetActive(true);

                GameObject coin2 = coinPooler.GeneratePlatform();
                coin2.transform.position = new Vector3(startPosition.x - distanceToCoin, startPosition.y, startPosition.z);
                coin2.SetActive(true);

                GameObject coin3 = coinPooler.GeneratePlatform();
                coin3.transform.position = new Vector3(startPosition.x + distanceToCoin, startPosition.y, startPosition.z);
                coin3.SetActive(true);
                break;

            case 1:
                GameObject crown1 = crownPooler.GeneratePlatform();
                crown1.transform.position = startPosition;
                crown1.SetActive(true);

                GameObject crown2 = crownPooler.GeneratePlatform();
                crown2.transform.position = new Vector3(startPosition.x - distanceToCrown, startPosition.y, startPosition.z);
                crown2.SetActive(true);
                break;

            case 2:
                GameObject ring1 = ringPooler.GeneratePlatform();
                ring1.transform.position = startPosition;
                ring1.SetActive(true);

                GameObject ring2 = ringPooler.GeneratePlatform();
                ring2.transform.position = new Vector3(startPosition.x - distanceToRing, startPosition.y, startPosition.z);
                ring2.SetActive(true);
                break;

            case 3:
                GameObject diamond1 = diamondPooler.GeneratePlatform();
                diamond1.transform.position = startPosition;
                diamond1.SetActive(true);
                break;
        }
    }
}