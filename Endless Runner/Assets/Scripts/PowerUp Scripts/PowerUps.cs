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
    public AudioSource playPowerupAudio;


    // Find Scripts
    private PowerUpManager m_PowerUpManager;

    public void Awake()
    {
        // Find Components
        playPowerupAudio = GetComponent<AudioSource>();
    }

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

    public void Update()
    {
        // Audio
        playPowerupAudio.clip = currentPowerupSound;    // Takes Audio From 'myPowerupObjects' Array
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
        // Power-Up Sound
        currentPowerupSound = myPowerupObjects[currentPowerupPicker].powerupSound;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Audio
        playPowerupAudio.Play();

        // Turns Off Sprite
        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Waits Set Amount Of Seconds
    IEnumerator WaitTill()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        // Goes where 'StartCoroutine("WaitTill");' Is
        m_PowerUpManager.ActivatePowerUp(playerFlightBool, noSpikesBool, currentPowerUpActiveTime);    // Sends The Selected Variables (At The Top) To the 'ActivatePowerUp' Method In The 'PowerUpManager' Script
        gameObject.SetActive(false);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        // Checks if touching a gameObject that has the name "Player"
        if (collision.name == "Player")
        {
            StartCoroutine("WaitTill");
        }

    }
}
