using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public Transform LevelGenerator;
    public EndlessPlayerController thePlayer;    // 'EndlessPlayerController' is a seperate script
    public DeathMenu theDeathScreen;

    private Vector3 platformStartPoint;
    private Vector3 playerStartpoint;
    private ObjectDestroyer[] platformList;      // 'ObjectDestroyer' is a seperate script
    private ScoreManager theScoreManager;        // 'ScoreManager' is a seperate script

    void Start()
    {
        platformStartPoint = LevelGenerator.position;
        playerStartpoint = thePlayer.transform.position;
        // Finds Componints on the 'ScoreManager' script
        theScoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {

    }

    public void RestartGame()
    {
        // Changing variables from the ScoreManager script
        theScoreManager.canScore = false;
        // Changing variables from the EndlessPlayerController script
        thePlayer.gameObject.SetActive(false);
        // Turns on the Death Screen
        theDeathScreen.gameObject.SetActive(true);

        //-StartCoroutine("RestartGameCo");
    }

    public void Reset()
    {
        Time.timeScale = 1f;

        // Turns off Death Screen
        theDeathScreen.gameObject.SetActive(false);

        // Finds Objects from 'ObjectDestroyer' script
        platformList = FindObjectsOfType<ObjectDestroyer>();

        // '.Length' finds the number thePltform is set to automaticly
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        thePlayer.transform.position = playerStartpoint;
        LevelGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);
        theScoreManager.scoredDistance = 0;    // Changing variables from the ScoreManager script
        theScoreManager.canScore = true;
    }


    /*public IEnumerator RestartGameCo()
    {
        // Changing variables from the ScoreManager script
        theScoreManager.canScore = false;
        // Changing variables from the EndlessPlayerController script
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        // Finds Objects from 'ObjectDestroyer' script
        platformList = FindObjectsOfType<ObjectDestroyer>();

        // '.Length' finds the number thePltform is set to automaticly
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        thePlayer.transform.position = playerStartpoint;
        LevelGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);
        // Changing variables from the ScoreManager script
        theScoreManager.scoreCount = 0;
        theScoreManager.canScore = true;
    }*/
}
