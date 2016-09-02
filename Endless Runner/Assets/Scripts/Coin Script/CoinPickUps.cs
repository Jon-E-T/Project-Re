using UnityEngine;
using System.Collections;

public class CoinPickUps : MonoBehaviour
{

    public int scoreFromCoin;    // Sets Coin Worth


    private ScoreManager m_ScoreManager;    // 'ScoreManager' is a seperate script
    private AudioSource coinPickUpSound;

    void Start()
    {
        // Finds the 'ScoreManager' script and its public variables
        m_ScoreManager = FindObjectOfType<ScoreManager>();
        // Finds GameObject ("NameOfGameObject") then Gets the AudioSurce Component
        coinPickUpSound = GameObject.Find("Coin Sound").GetComponent<AudioSource>();
    }

    // 'other' is the is a name for when this collider and other colliders touch 
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

            // m_ScoreManager is refering to the 'ScoreManager' scrip
            // '.AddToScore' is a calling method in that script
            // '(scoreFormCoin)' on this script replaces '(pointsToAdd)' in the 'ScoreManager' script
            m_ScoreManager.AddToScore(scoreFromCoin);
            gameObject.SetActive(false);
        }
    }
}
