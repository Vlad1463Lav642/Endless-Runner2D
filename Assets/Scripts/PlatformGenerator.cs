using UnityEngine;

/// <summary>
/// Обеспечивает генерацию платформ по пути движения игрока.
/// </summary>
public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private Transform generationPoint;

    [SerializeField] private float distanceToPlatform;

    [SerializeField] private float distanceToPlatformMin;
    [SerializeField] private float distanceToPlatformMax;

    [SerializeField] private Pooler[] pooledGameObjects;

    private int currentPlatformID;
    private float[] platformWidths;

    private float minHeight;
    [SerializeField] private Transform maxHeightPoint;
    private float maxHeight;
    [SerializeField] private float maxHeightChange;
    private float heightChange;

    [SerializeField] private float coinHeightBetweenPlatform;
    [SerializeField] private float spikeHeightBetweenPlatform;
    [SerializeField] private float heartHeightBetweenPlatform;
    [SerializeField] private float scrollHeightBetweenPlatform;

    private CoinGenerator coinGenerator;
    [SerializeField] private float randomCoinLimit;

    [SerializeField] private float randomHeartLimit;
    [SerializeField] private Pooler heartPool;

    [SerializeField] private float randomScrollLimit;
    [SerializeField] private Pooler scrollPool;

    [SerializeField] private float randomSpikeLimit;
    [SerializeField] private Pooler spikePool;

    private void Start()
    {
        platformWidths = new float[pooledGameObjects.Length];

        for(int i = 0; i<pooledGameObjects.Length; i++)
        {
            platformWidths[i] = pooledGameObjects[i].GetPooledGameObject().GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        coinGenerator = FindObjectOfType<CoinGenerator>();
    }

    private void Update()
    {
        if(transform.position.x < generationPoint.position.x)
        {
            distanceToPlatform = Random.Range(distanceToPlatformMin, distanceToPlatformMax);

            currentPlatformID = Random.Range(0, pooledGameObjects.Length);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if(heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }else if(heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[currentPlatformID] / 2) + distanceToPlatform, heightChange, transform.position.z);

            GameObject newPlatform = pooledGameObjects[currentPlatformID].GeneratePlatform();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if(Random.Range(0f,100f) < randomCoinLimit)
            {
                coinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + coinHeightBetweenPlatform, transform.position.z));
            }

            if(Random.Range(0f,100f) < randomSpikeLimit)
            {
                GameObject newSpike = spikePool.GeneratePlatform();

                float spikeXPosition = Random.Range(-platformWidths[currentPlatformID] / 2 + 4f, platformWidths[currentPlatformID] / 2 - 4f);

                Vector3 spikePosition = new Vector3(spikeXPosition, 0.5f + spikeHeightBetweenPlatform, 0f);

                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
            }

            if(Random.Range(0f,100f) < randomScrollLimit)
            {
                GameObject newScroll = scrollPool.GeneratePlatform();

                float scrollXPosition = Random.Range(-platformWidths[currentPlatformID] / 2 + 1f, platformWidths[currentPlatformID] / 2 - 1f);

                Vector3 scrollPosition = new Vector3(scrollXPosition, 0.5f + scrollHeightBetweenPlatform, 0f);

                newScroll.transform.position = transform.position + scrollPosition;
                newScroll.transform.rotation = transform.rotation;
                newScroll.SetActive(true);
            }

            if(Random.Range(0f,100f) < randomHeartLimit)
            {
                GameObject newHeart = heartPool.GeneratePlatform();

                float heartXPosition = Random.Range(-platformWidths[currentPlatformID] / 2 + 1f, platformWidths[currentPlatformID] / 2 - 1f);

                Vector3 heartPosition = new Vector3(heartXPosition, 0.5f + heartHeightBetweenPlatform, 0f);

                newHeart.transform.position = transform.position + heartPosition;
                newHeart.transform.rotation = transform.rotation;
                newHeart.SetActive(true);
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[currentPlatformID] / 2), transform.position.y, transform.position.z);
        }
    }
}