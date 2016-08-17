using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Objects Measured For Distance
    public Transform player;
    public Transform startPoint;
    // OnScreen Display Text
    public Text scoreText;
    public Text highScoreText;
    // OnScreen Display Text float
    public float scoredDistance;
    public float highScoredDistance;
    //-public float pointsPerSecond;
    public bool canScore;    // Can The Player Score


    private float dist;


    void Start()
    {
        // Retrieves the Players High Score
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoredDistance = PlayerPrefs.GetFloat("HighScore");
            // To Delete High Score ste 'PlayerPrefs'-("HighScore") to 0
        }
    }

    void Update()
    {
        FindDistance();
        ScoreCounter();
    }

    // Whatever is the brackets of AddToScore(___); when calling a method will replace pointsToAdd
    public void AddToScore(int pointsToAdd)
    {
        scoredDistance += pointsToAdd;
        if (scoredDistance < 0)
            scoredDistance = 0;
    }

    void FindDistance()
    {
        dist = Vector2.Distance(player.position, startPoint.transform.position);
    }

    void ScoreCounter()
    {
        if (canScore || FindObjectOfType<EndlessPlayerController>().moveSpeed > 0)
        {
            // Score Incresses Based On How Far The Player Went
            // Distance = Time * Speed
            scoredDistance += dist * Time.deltaTime * FindObjectOfType<EndlessPlayerController>().moveSpeed / 100;
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
        highScoreText.text = "Best " + Mathf.Round(highScoredDistance) + "m";    // 'highScoreText' is the name of the variable and '.text' edits the text of the variable
    }
}
