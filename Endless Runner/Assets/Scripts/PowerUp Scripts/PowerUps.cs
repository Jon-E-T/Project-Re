using UnityEngine;
using System.Collections;

public class PowerUps : MonoBehaviour
{
    // Power-Ups
    public bool playerFlying;
    public bool noSpikes;
    // Power-Up Leangth
    public float powerUpActiveTime;
    // Sprite Change
    public Sprite[] powerupSprites;


    // Find Scripts
    private PowerUpManager thePowerUpManager;


    void Start()
    {
        thePowerUpManager = FindObjectOfType<PowerUpManager>();
    }

    // Awake is called when the script instance is being loaded
    public void Awake()
    {
        //PowerUpPicker();
    }

    // This function is called when the object becomes enabled and active
    public void OnEnable()
    {
        PowerUpPicker();
    }


    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if touching a gameObject that has the name "Player"
        if (collision.name == "Player")
        {
            thePowerUpManager.ActivatePowerUp(playerFlying, noSpikes, powerUpActiveTime);    // Sends The Selected Variables (At The Top) To the 'ActivatePowerUp' Method In The 'PowerUpManager' Script 
        }
        gameObject.SetActive(false);
    }

    void PowerUpPicker()
    {
        // Randomly Pick A Power-Up
        int powerUpSelector = Random.Range(0, 2);    // Random.Range int Wont Pick Max Number
        switch (powerUpSelector)
        {
            case 0:
                playerFlying = true;
                noSpikes = false;
                powerUpActiveTime = 5;
                break;
            case 1:
                noSpikes = true;
                playerFlying = false;
                powerUpActiveTime = 5;
                break;
        }

        // Sprite Selector
        GetComponent<SpriteRenderer>().sprite = powerupSprites[powerUpSelector];    // Takes Number From 'PowerUpSelector'

    }
}
