using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    // Sounds
    public AudioSource powerupPlayerFlySound;
    public AudioSource powerupNoSpikesSound;


    // Copied From 'PowerUps' Script
    private bool playerFlightBool;
    private bool noSpikesBool;
    private float currentPowerUpActiveTime;
    // Not Copied
    // Is The Power-Up Active
    [HideInInspector] public bool isPowerUpActive;
    // Find Scripts
    private ScoreManager m_ScoreManager;
    private LevelGenerator m_LevelGenerator;
    private EndlessPlayerController m_EndlessPlayerController;
    private PlayerJumpController m_PlayerJumpController;
    private PowerUps m_PowerUps;
    // Original Variables 
    private float originalSpikeFrequency;
    private float originalMoveSpeed;
    private float originalPlatformMax;
    private float originalPlatformMin;
    private float originalPlatformHeightChange;
    private float originalGravityScale;
    // Find Game Object
    private GameObject player;
    // Destroyer Arrays
    private ObjectDestroyer[] spikeList;
    private ObjectDestroyer[] powerupList;
    // Sounds
    private bool playSounds;
    

    void Start()
    {
        // Find Scripts
        m_ScoreManager = FindObjectOfType<ScoreManager>();
        m_LevelGenerator = FindObjectOfType<LevelGenerator>();
        m_EndlessPlayerController = FindObjectOfType<EndlessPlayerController>();
        m_PlayerJumpController = FindObjectOfType<PlayerJumpController>();
        m_PowerUps = FindObjectOfType<PowerUps>();

        // Find Objects
        player = GameObject.Find("Player");

        // Find Original Variables
            // Platforms
        originalPlatformMax = m_LevelGenerator.distanceBetweenPlatformsMax;
        originalPlatformMin = m_LevelGenerator.distanceBetweenPlatformsMin;
        originalPlatformHeightChange = m_LevelGenerator.maxHeightChange;
            // World
        originalGravityScale = player.GetComponent<Rigidbody2D>().gravityScale;

        // Sounds
        playSounds = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (isPowerUpActive)
        {
            currentPowerUpActiveTime -= Time.deltaTime;

            FlyingPlayer();

            noSpikesBoolPowerUp();

            DisablePowerUps();
        }
    }

    public void ActivatePowerUp(bool pFly, bool nSpikes, float pUpActiveTime)
    {
        playerFlightBool = pFly;
        noSpikesBool = nSpikes;
        currentPowerUpActiveTime = pUpActiveTime;

        originalMoveSpeed = m_EndlessPlayerController.moveSpeed;

        originalSpikeFrequency = m_LevelGenerator.frequencyOfSpikes;

        isPowerUpActive = true;
    }

    void FlyingPlayer()
    {
        if (playerFlightBool)
        {
            m_EndlessPlayerController.MovePlayerToTop();    // Moves Player To Top Of The Screen

            // Changes To Player
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            //m_EndlessPlayerController.moveSpeed = originalMoveSpeed;
            player.GetComponent<BoxCollider2D>().isTrigger = true;
            m_PlayerJumpController.canDoubleJump = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector3(m_EndlessPlayerController.moveSpeed, 0f, 0f);

            // Changes To Platforms
            m_LevelGenerator.distanceBetweenPlatformsMax = 0;
            m_LevelGenerator.distanceBetweenPlatformsMin = 0;
            m_LevelGenerator.maxHeightChange = 0;

            // Removes Spikes
            SpikeDeSpawner();

            // Sound
            if (playSounds == true)
            {
                powerupPlayerFlySound.Play();
                playSounds = false;
            }

            PowerUpDeSpawner();    // DeSpawns Power-Ups
        }
    }

    void noSpikesBoolPowerUp()
    {
        if (noSpikesBool)
        {
            m_LevelGenerator.frequencyOfSpikes = 0;

            SpikeDeSpawner();

            //// Sound
            if (playSounds)
            {
                powerupNoSpikesSound.Play();
                playSounds = false;
            }

            PowerUpDeSpawner();    // DeSpawns Power-Ups
        }
    }

    void DisablePowerUps()
    {
        // Turns Off Current Enabled Power-Up
        if (currentPowerUpActiveTime <= 0 || !m_ScoreManager.canScore)
        {
            if (playerFlightBool)
            {
                // Changes To Player
                player.GetComponent<BoxCollider2D>().isTrigger = false;
                player.GetComponent<Rigidbody2D>().gravityScale = originalGravityScale;
                //m_EndlessPlayerController.moveSpeed = originalMoveSpeed;

                // Changes To Platforms
                m_LevelGenerator.distanceBetweenPlatformsMax = originalPlatformMax;
                m_LevelGenerator.distanceBetweenPlatformsMin = originalPlatformMin;
                m_LevelGenerator.maxHeightChange = originalPlatformHeightChange;
            }

            if (noSpikesBool)
            {
                m_LevelGenerator.frequencyOfSpikes = originalSpikeFrequency;
            }

            playSounds = true;
            isPowerUpActive = false;
        }
    }

    void PowerUpDeSpawner()
    {
        // Remove Current Power-Ups
        powerupList = FindObjectsOfType<ObjectDestroyer>();
        for (int i = 0; i < powerupList.Length; i++)
        {
            if (powerupList[i].gameObject.name.Contains("Power-Up"))
            {
                powerupList[i].gameObject.SetActive(false);
            }
        }
    }

    void SpikeDeSpawner()
    {
        // Remove Current Spikes
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
