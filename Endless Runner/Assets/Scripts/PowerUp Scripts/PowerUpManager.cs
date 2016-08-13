using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    // Copied From 'PowerUps' Script
    private bool moreGravity;
    private bool noSpikes;
    private float powerUpActiveTime;
    // Not Copied
    private bool powerUpActive;
    private ScoreManager theScoreManager;
    private PlatformGenerator thePlatformGenerator;
    // Original Variables 
    private float originalGravityScale;
    private float originalSpikeFrequency;
    private ObjectDestroyer[] spikeList;
    // Find Game Object
    private GameObject player;


    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpActive)
        {
            powerUpActiveTime -= Time.deltaTime;

            MoreGravityPowerUp();

            NoSpikesPowerUp();

            DisablePowerUps();
        }
    }

    public void ActivatePowerUp(bool mGravity, bool nSpikes, float pUpActiveTime)
    {
        moreGravity = mGravity;
        noSpikes = nSpikes;
        powerUpActiveTime = pUpActiveTime;

        originalGravityScale = player.GetComponent<Rigidbody2D>().gravityScale;

        originalSpikeFrequency = thePlatformGenerator.frequencyOfSpikes;

        powerUpActive = true;
    }

    void MoreGravityPowerUp()
    {
        if (moreGravity)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                player.GetComponent<Rigidbody2D>().gravityScale = originalGravityScale;
            }
            else
            {
                player.GetComponent<Rigidbody2D>().gravityScale = 10;
            }
        }
    }

    void NoSpikesPowerUp()
    {
        if (noSpikes)
        {
            thePlatformGenerator.frequencyOfSpikes = 0;

            // Spike DeSpawner
            spikeList = FindObjectsOfType<ObjectDestroyer>();
            for (int i = 0; i < spikeList.Length; i++)
            {
                if (spikeList[i].gameObject.name.Contains("Spikes"))
                {
                    spikeList[i].gameObject.SetActive(false);
                }
            }
        }
    }

    void DisablePowerUps()
    {
        if (powerUpActiveTime <= 0 || !theScoreManager.canScore)
        {
            player.GetComponent<Rigidbody2D>().gravityScale = originalGravityScale;

            thePlatformGenerator.frequencyOfSpikes = originalSpikeFrequency;

            powerUpActive = false;
        }
    }

}
