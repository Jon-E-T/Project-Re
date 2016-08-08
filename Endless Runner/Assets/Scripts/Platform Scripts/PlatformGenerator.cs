using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour
{
    // use Prefab GameObject!
    // [] turns any variable into an array
    public Transform generationPoint;
    public float distanceBetween;
    public float distanceBetweenMin;
    public float distanceBetweenMax;
    // 'ObjectPool' is a seperat script 
    public ObjectPooler[] theObjectPool;
    public Transform maxHeightPoint;
    public float maxHeightChange;
    public float frequencyOfCoins;
    public ObjectPooler spikePool;
    public float frequencyOfSpikes;

    private float[] platformWidth;
    private int platformSelector;
    private float minHeight;
    private float maxHeight;
    private float heightChange;
    // 'CoinGenerator' is a seperat script 
    private CoinGenerator theCoinGen;

    void Start()
    {
        // '.Length' finds the number thePltform is set to automaticly
        platformWidth = new float[theObjectPool.Length];
        for (int i = 0; i < theObjectPool.Length; i++)
        {
            // '.pooledObject' is from objectPooler script
            platformWidth[i] = theObjectPool[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
        // Finds 'CoinGenerator' Script
        theCoinGen = FindObjectOfType<CoinGenerator>();
    }

    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            // platformSelector is a private int, the picks from a number Random Range of numbers between 0 and whatever number theObjectPool is set to
            // '.Length' finds the number 'thePltform' is set to automaticly
            platformSelector = Random.Range(0, theObjectPool.Length);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);
            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            transform.position = new Vector3(transform.position.x + (platformWidth[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

            // Instantiate Copy and paste whatever GameObject is in "public GameObject thePlarform;"
            // '[platformSelector]' from the Random Range already detrmied by the "platformSelector = Random.Range(0, thePlatform.Length);" code above
            //-Instantiate(thePlatform[platformSelector], transform.position, transform.rotation);

            GameObject newPlatform = theObjectPool[platformSelector].GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            // If the Variable set for 'frequencyOfCoins' is greater then a randomly picked number between 0-100
            if (Random.Range(0f, 100f) < frequencyOfCoins)
            {
                // Gold Coin
                if (theCoinGen.coinSelector == 0)
                {
                    // 'SpawnCoins' is a method from 'CoinGenerator' script
                    theCoinGen.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
                }
                // Bronze Coins
                //else if (theCoinGen.coinSelector == 1)
                //{
                //    // 'SpawnCoins' is a method from 'CoinGenerator' script
                //    theCoinGen.SpawnCoins(new Vector3(transform.position.x + Random.Range(-3,3), transform.position.y + 1f, transform.position.z));
                //}

            }

            // If the Variable set for 'frequencyOfSpikes' is greater then a randomly picked number between 0-100
            if (Random.Range (0f, 100f) < frequencyOfSpikes)
            {
                GameObject newSpike = spikePool.GetPooledObject();

                // Moves Spikes into Position
                Vector3 spikePosition = new Vector3(0f, 0.5f, 0f);
                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
            }

            transform.position = new Vector3(transform.position.x + (platformWidth[platformSelector] / 2), transform.position.y, transform.position.z);

        }
    }
}
