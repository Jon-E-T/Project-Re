using UnityEngine;
using System.Collections;

public class CoinPickUps : MonoBehaviour
{

    public int scoreFromCoin;    // Sets Coin Worth


    private ScoreManager m_ScoreManager;    // 'ScoreManager' is a seperate script
    private AudioSource coinPickUpSound;

    void Start()
    {
        // Find Scripts
        m_ScoreManager = FindObjectOfType<ScoreManager>();

        // Find Audio
        coinPickUpSound = GameObject.Find("Coin Sound").GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Checks if 'Collider2D' is touching a gameObject that has the name "Player"
        if (other.gameObject.name == "Player")
        {
            // Makes sure CoinSound plays on every coin
            if (coinPickUpSound.isPlaying)
            {
                coinPickUpSound.Stop();
                coinPickUpSound.Play();
            }
            else
            {
                coinPickUpSound.Play();
            }

            m_ScoreManager.AddToCoins(scoreFromCoin);    // Adds Coins To Score
            gameObject.SetActive(false);
        }
    }
}
