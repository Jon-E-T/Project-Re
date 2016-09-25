using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Objects Measured For Distance
    public Transform m_Player;
    // OnScreen Display Text
    public Text scoreText;
    public Text highScoreText;
    public Text coinCountText;
    // Score
    public float scoredDistance;    // Current Distance The Player Went
    // Coins
    //[HideInInspector]
    public int currentAmountOfCoins;    // Coins Player Currently Has
    // Player Prefs
    [HideInInspector]
    public float highScoredDistance;
    [HideInInspector]
    public int playerCoins;
    // bools
    [HideInInspector]
    public bool canScore;    // Can The Player Score
    public float scoredDistanceMultiplier = 1;


    private float dist;


    void Start()
    {
        PlayerPrefsRetrever();
    }

    // Finds Player Prefs
    void PlayerPrefsRetrever()
    {
        // Retrieves the Players High Score
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoredDistance = PlayerPrefs.GetFloat("HighScore");
            // To Delete High Score ste 'PlayerPrefs'-("HighScore") to 0
        }

        // Retrieves the Players Coins
        if (PlayerPrefs.HasKey("CoinScore"))
        {
            playerCoins = PlayerPrefs.GetInt("CoinScore");    // Sets 'playerCoins' To What Ever Was Saved
        }
    }

    void Update()
    {
        FindDistance();
        ScoreCounter();
        CoinCouner();
    }

    public void AddToScore(int pointsToAdd)
    {
        scoredDistance += pointsToAdd;
        if (scoredDistance < 0)
            scoredDistance = 0;
    }

    public void AddToCoins(int coinsToAdd)
    {
        currentAmountOfCoins += coinsToAdd;
        if (currentAmountOfCoins < 0)
            currentAmountOfCoins = 0;
    }

    void FindDistance()
    {
        //dist = Vector2.Distance(m_Player.position, startPoint.transform.position);
        dist = m_Player.GetComponent<Rigidbody2D>().velocity.x * Time.deltaTime;
    }

    void ScoreCounter()
    {
        if (canScore || FindObjectOfType<EndlessPlayerController>() != null)    // Can The Player Score?
        {
            // If Plyers Velocity is Grater then 0
            if (m_Player.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                // Score Incresses Based On How Far The Player Went
                // Distance = Time * Speed
                scoredDistance += dist * scoredDistanceMultiplier;
            }
        }

        if (scoredDistance > highScoredDistance)
        {
            highScoredDistance = scoredDistance;    // Sets highScore
            PlayerPrefs.SetFloat("HighScore", highScoredDistance);    // Saves the Player High Score
            // To Delete High Score ste 'PlayerPrefs-("HighScore")' to 0
        }

        // Displays Score
        scoreText.text = Mathf.Round(scoredDistance) + "m";    // 'scoreText' is the name of the variable and '.text' edits the text of the variable

        // Displays High Score
        highScoreText.text = "BEST:" + Mathf.Round(highScoredDistance);    // 'highScoreText' is the name of the variable and '.text' edits the text of the variable
    }

    void CoinCouner()
    {
        if (playerCoins > currentAmountOfCoins)
        {
            PlayerPrefs.SetInt("CoinScore", currentAmountOfCoins);
        }

        // Display Coin #
        coinCountText.text = currentAmountOfCoins.ToString();
    }
}
