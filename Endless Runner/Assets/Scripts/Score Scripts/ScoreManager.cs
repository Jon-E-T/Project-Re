using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text scoreText;
    public Text highScoreText;
    public float scoreCount;
    public float highScoreCount;
    public float pointsPerSecond;
    public bool canScore;

    void Start()
    {
        // Retrieves the Players High Score
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
            // To Delete High Score ste 'PlayerPrefs'-("HighScore") to 0
        }
    }

    void Update()
    {
        if(canScore)
        {
            // Adds whatever pointsPerSecond is set to
            scoreCount += pointsPerSecond * Time.deltaTime;
        }

        if (scoreCount > highScoreCount)
        {
            // Sets highScore
            highScoreCount = scoreCount;
            // Saves the Player High Score
            PlayerPrefs.SetFloat("HighScore", highScoreCount);
            // To Delete High Score ste 'PlayerPrefs-("HighScore")' to 0
        }

        // 'scoreText' is the name of the variable and '.text' edits the text of the variable
        // 'Mathf.Round()' Rounds to a whole number
        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        // 'highScoreText' is the name of the variable and '.text' edits the text of the variable
        highScoreText.text = "High Score: " + Mathf.Round(highScoreCount);
    }

    // Whatever is the brackets of AddToScore(___); when calling a method will replace pointsToAdd
    public void AddToScore(int pointsToAdd)
    {
        scoreCount += pointsToAdd;
        if (scoreCount < 0)
            scoreCount = 0;
    }
}
