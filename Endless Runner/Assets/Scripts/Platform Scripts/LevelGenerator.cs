using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    // use Prefab GameObject!
    // [] turns any variable into an array

    // Find Screen Points 
    public Transform m_TopOfScreenRight;         // Find The Top Right Of The Screen
    // Clouds        
    public Transform generationPoint;
    public ObjectPooler cloudPool;
    public float frequencyOfClouds;
    // Distance Between Platforms
    [HideInInspector] public float distanceBetweenPlatforms;
    public float distanceBetweenPlatformsMin;
    public float distanceBetweenPlatformsMax;
    // Platform Height Change
    public Transform maxHeightPoint;
    public float maxHeightChange;
    public ObjectPooler[] theObjectPool;    // 'ObjectPool' is a seperat script 
    // Coins
    public float frequencyOfCoins;
    // Spikes
    public ObjectPooler spikePool;
    public float frequencyOfSpikes;
    // Power-Ups
    public ObjectPooler powerupPool;
    public float powerupHeight;
    public float powerupHeightMax = 6f;
    public float frequencyOfPowerups;


    private float[] platformWidth;
    private int platformSelector;
    // Platform Height
    private float minHeight;
    private float maxHeight;
    private float heightChange;
    // Find Scripts
    private CoinGenerator m_CoinGen;       // 'CoinGenerator' is a seperat script
    private EndlessPlayerController m_EndlessPlayerController;
    private PowerUpManager m_PowerUpManager;
    // Find Objects
    private GameObject m_NewPowerupHeight;
    private GameObject m_Player;

    void Start()
    {
        // '.Length' finds the number thePltform is set to automaticly
        platformWidth = new float[theObjectPool.Length];
        for (int i = 0; i < theObjectPool.Length; i++)
        {
            // '.pooledObject' is from objectPooler script
            platformWidth[i] = theObjectPool[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        // Set Variables
        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        // Finds Scripts
        m_CoinGen = FindObjectOfType<CoinGenerator>();
        m_EndlessPlayerController = FindObjectOfType<EndlessPlayerController>();
        m_PowerUpManager = FindObjectOfType<PowerUpManager>();

        // Find Objects
        m_Player = GameObject.Find("Player");

    }

    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetweenPlatforms = Random.Range(distanceBetweenPlatformsMin, distanceBetweenPlatformsMax);

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

            //PowerUpSpawn();

            transform.position = new Vector3(transform.position.x + (platformWidth[platformSelector] / 2) + distanceBetweenPlatforms, heightChange, transform.position.z);

            // Instantiate Copy and paste whatever GameObject is in "public GameObject thePlarform;"
            // '[platformSelector]' from the Random Range already detrmied by the "platformSelector = Random.Range(0, thePlatform.Length);" code above
            //-Instantiate(thePlatform[platformSelector], transform.position, transform.rotation);

            GameObject newPlatform = theObjectPool[platformSelector].GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);


            CoinSpawn();

            SpikeSpawn();

            CloudSpawn();

            transform.position = new Vector3(transform.position.x + (platformWidth[platformSelector] / 2), transform.position.y, transform.position.z);
        }
    }

    void CloudSpawn()
    {
        if (Random.Range(0f, 100f) < frequencyOfClouds)
        {
            // Find Objects
            GameObject newCloud = cloudPool.GetPooledObject();
            newCloud.name = "Cloud";

            // Cloud Spawn
            if (m_Player.transform.position.y > 6)
            {
                newCloud.transform.position = new Vector3(transform.position.x + Random.Range(-10f, 10f) / Random.Range(1f, 10f), Random.Range(6f, 10f), Random.Range(-1, 2));   // Cloud Spawn Height Cap 10
            }
            else if (m_Player.transform.position.y <= 6)
            {
                newCloud.transform.position = new Vector3(transform.position.x + Random.Range(-10f, 10f) / Random.Range(1f, 10f), Random.Range(6f, 7.5f), Random.Range(-1, 2));  // Cloud Spawn Height Cap 7.5
            }

            newCloud.SetActive(true);
        }
    }

    public void PowerUpSpawn()
    {
        if (Random.Range(0f, 100f) < frequencyOfPowerups && !m_PowerUpManager.isPowerUpActive)    // Power-Up Spawn Conditions
        {
            // Power-Up Spawn Point
            GameObject newPowerUp = powerupPool.GetPooledObject();
            newPowerUp.transform.position = transform.position + new Vector3(distanceBetweenPlatforms / 2f, Random.Range(2f, powerupHeight), 0f);
            newPowerUp.SetActive(true);
            m_NewPowerupHeight = newPowerUp;

            // Power-Up Spawn Modifiers
            // Power-Up Height
            if (m_NewPowerupHeight.transform.position.y >= 7)    // Stops Power-Ups From spawning Above The Players FOV
            {
                newPowerUp.transform.position = new Vector3(transform.position.x, powerupHeightMax, transform.position.z);
            }
        }

    }

    void CoinSpawn()
    {
        // If the Variable set for 'frequencyOfCoins' is greater then a randomly picked number between 0-100
        if (Random.Range(0f, 100f) < frequencyOfCoins)
        {
            // Gold Coin
            if (m_CoinGen.coinSelector == 0)
            {
                // 'SpawnCoins' is a method from 'CoinGenerator' script
                m_CoinGen.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z));
            }
            // Bronze Coins
            //else if (m_CoinGen.coinSelector == 1)
            //{
            //    // 'SpawnCoins' is a method from 'CoinGenerator' script
            //    m_CoinGen.SpawnCoins(new Vector3(transform.position.x + Random.Range(-3,3), transform.position.y + 1f, transform.position.z));
            //}
        }

    }

    void SpikeSpawn()
    {
        // If the Variable set for 'frequencyOfSpikes' is greater then a randomly picked number between 0-100
        // And if platformWidth is Grater Then 3
        if (Random.Range(0f, 100f) < frequencyOfSpikes && platformWidth[platformSelector] > 3)
        {
            GameObject newSpike = spikePool.GetPooledObject();

            float spikeXPosition = Random.Range(-platformWidth[platformSelector] / 2 + 1f, platformWidth[platformSelector] / 2 - 1f);    // Random X value of current Platform

            // Moves Spikes into Position
            Vector3 spikePosition = new Vector3(spikeXPosition, 0.5f, 0f);
            newSpike.transform.position = transform.position + spikePosition;
            newSpike.transform.rotation = transform.rotation;
            newSpike.SetActive(true);
        }
    }
}
