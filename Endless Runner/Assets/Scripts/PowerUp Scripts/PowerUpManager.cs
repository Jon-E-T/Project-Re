﻿using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    // Copied From 'PowerUps' Script
    private bool playerFlying;
    private bool noSpikes;
    private float powerUpActiveTime;
    // Not Copied
    public bool isPowerUpActive;
    // Find Scripts
    private ScoreManager theScoreManager;
    private LevelGenerator theLevelGenerator;
    private EndlessPlayerController theEndlessPlayerController;
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
    private AudioSource powerupPlayerFlySound;
    private AudioSource powerupNoSpikesSound;


    void Start()
    {
        // Find Scripts
        theScoreManager = FindObjectOfType<ScoreManager>();
        theLevelGenerator = FindObjectOfType<LevelGenerator>();
        theEndlessPlayerController = FindObjectOfType<EndlessPlayerController>();

        // Find Objects
        player = GameObject.Find("Player");
        powerupPlayerFlySound = GameObject.Find("PlayerFly Sound").GetComponent<AudioSource>();
        powerupNoSpikesSound = GameObject.Find("NoSpikes Sound").GetComponent<AudioSource>();

        // Find Original Variables
            // Platforms
        originalPlatformMax = theLevelGenerator.distanceBetweenPlatformsMax;
        originalPlatformMin = theLevelGenerator.distanceBetweenPlatformsMin;
        originalPlatformHeightChange = theLevelGenerator.maxHeightChange;
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
            powerUpActiveTime -= Time.deltaTime;

            FlyingPlayer();

            NoSpikesPowerUp();

            DisablePowerUps();
        }
    }

    public void ActivatePowerUp(bool pFly, bool nSpikes, float pUpActiveTime)
    {
        playerFlying = pFly;
        noSpikes = nSpikes;
        powerUpActiveTime = pUpActiveTime;

        originalMoveSpeed = theEndlessPlayerController.moveSpeed;

        originalSpikeFrequency = theLevelGenerator.frequencyOfSpikes;

        isPowerUpActive = true;
    }

    void FlyingPlayer()
    {
        if (playerFlying)
        {
            theEndlessPlayerController.MovePlayerToTop();    // Moves Player To Top Of The Screen

            // Changes To Player
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            //theEndlessPlayerController.moveSpeed = originalMoveSpeed;
            player.GetComponent<BoxCollider2D>().isTrigger = true;
            theEndlessPlayerController.canDoubleJump = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector3(theEndlessPlayerController.moveSpeed, 0f, 0f);

            // Changes To Platforms
            theLevelGenerator.distanceBetweenPlatformsMax = 0;
            theLevelGenerator.distanceBetweenPlatformsMin = 0;
            theLevelGenerator.maxHeightChange = 0;

            // Sound
            if (playSounds == true)
            {
                powerupPlayerFlySound.Play();
                playSounds = false;
            }

            PowerUpDeSpawner();    // DeSpawns Power-Ups
        }
    }

    void NoSpikesPowerUp()
    {
        if (noSpikes)
        {
            theLevelGenerator.frequencyOfSpikes = 0;

            // Spike DeSpawner
            spikeList = FindObjectsOfType<ObjectDestroyer>();
            for (int i = 0; i < spikeList.Length; i++)
            {
                if (spikeList[i].gameObject.name.Contains("Spikes"))
                {
                    spikeList[i].gameObject.SetActive(false);
                }
            }

            // Sound
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
        if (powerUpActiveTime <= 0 || !theScoreManager.canScore)
        {
            if (playerFlying)
            {
                // Changes To Player
                player.GetComponent<BoxCollider2D>().isTrigger = false;
                player.GetComponent<Rigidbody2D>().gravityScale = originalGravityScale;
                //theEndlessPlayerController.moveSpeed = originalMoveSpeed;

                // Changes To Platforms
                theLevelGenerator.distanceBetweenPlatformsMax = originalPlatformMax;
                theLevelGenerator.distanceBetweenPlatformsMin = originalPlatformMin;
                theLevelGenerator.maxHeightChange = originalPlatformHeightChange;
            }

            if (noSpikes)
            {
                theLevelGenerator.frequencyOfSpikes = originalSpikeFrequency;
            }

            playSounds = true;
            isPowerUpActive = false;
        }
    }

    void PowerUpDeSpawner()
    {
        //Power-Up DeSpwaner
        powerupList = FindObjectsOfType<ObjectDestroyer>();
        for (int i = 0; i < powerupList.Length; i++)
        {
            if (powerupList[i].gameObject.name.Contains("Power-Up"))
            {
                powerupList[i].gameObject.SetActive(false);
            }
        }
    }

}
