using UnityEngine;
using System.Collections;

public class PowerUps : MonoBehaviour
{
    // Power-Ups
    public bool moreGravity;
    public bool noSpikes;
    // Power-Up Leangth
    public float powerUpActiveTime;
    // Sprite Change
    public Sprite[] powerupSprites;

    private PowerUpManager thePowerUpManager;

    void Start()
    {
        thePowerUpManager = FindObjectOfType<PowerUpManager>();
    }

    // Awake is called when the script instance is being loaded
    public void Awake()
    {
        PowerUpPicker();
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if touching a gameObject that has the name "Player"
        if (collision.name == "Player")
        {
            thePowerUpManager.ActivatePowerUp(moreGravity, noSpikes, powerUpActiveTime);    // Sends The Selected Variables (At The Top) To the 'ActivatePowerUp' Method In The 'PowerUpManager' Script 
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
                moreGravity = true;
                powerUpActiveTime = 10;
                break;
            case 1:
                noSpikes = true;
                powerUpActiveTime = 5;
                break;
        }

        // Sprite Selector
        GetComponent<SpriteRenderer>().sprite = powerupSprites[powerUpSelector];    // Takes Number From 'PowerUpSelector'

    }
}
