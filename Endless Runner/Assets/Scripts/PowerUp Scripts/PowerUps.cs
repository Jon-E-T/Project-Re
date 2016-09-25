using UnityEngine;
using System.Collections;

public class PowerUps : MonoBehaviour
{
    // Power-Ups
    public string currentPowerupName;
    public bool playerFlightBool;
    public bool noSpikesBool;
    // Power-Up Leangth
    public float currentPowerUpActiveTime;
    // Sprite Change
    public Sprite currentPowerupSprites;
    public Color currentPowerupColor;
    // Scriptable Objects
    public PowerupItemData[] myPowerupObjects;
    // Array Picker
    public int currentPowerupPicker;
    // Audio
    public AudioClip currentPowerupSound;
    // Find Components


    // Find Scripts
    private PowerUpManager m_PowerUpManager;


    public void OnEnable()
    {
        // Turns On Sprite
        GetComponent<SpriteRenderer>().enabled = true;

        PowerupItem();    // Changes Current Power-Up
    }

    void Start()
    {
        // Find Scripts
        m_PowerUpManager = FindObjectOfType<PowerUpManager>();
    }

    // Power-Up Config
    void PowerupItem()
    {
        // Randomly Pick A Power-Up
        currentPowerupPicker = Random.Range(0, myPowerupObjects.Length);

        // Power-Up Name
        currentPowerupName = myPowerupObjects[currentPowerupPicker].powerupName;
        // Change Power-Up GameObject Name
        gameObject.name = currentPowerupName;
        // Power-Up Sprite
        currentPowerupSprites = myPowerupObjects[currentPowerupPicker].powerupSprite;
        // Power-Up Color
        currentPowerupColor = myPowerupObjects[currentPowerupPicker].powerupColor;
        // Applys Power-Up Color
        GetComponent<SpriteRenderer>().color = currentPowerupColor;
        // Power-Up Player Flight
        playerFlightBool = myPowerupObjects[currentPowerupPicker].playerFlight;
        // Power-Up No Spikes
        noSpikesBool = myPowerupObjects[currentPowerupPicker].noSpikes;
        // Power-Up Active Time
        currentPowerUpActiveTime = myPowerupObjects[currentPowerupPicker].powerUpActiveTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        m_PowerUpManager.ActivatePowerUp(playerFlightBool, noSpikesBool, currentPowerUpActiveTime);    // Sends The Selected Variables (At The Top) To the 'ActivatePowerUp' Method In The 'PowerUpManager' Script
        gameObject.SetActive(false);
    }
}
