using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Find GameObject
    public Transform m_LevelGenerator;
    // Find Scripts
    public EndlessPlayerController m_Player;    // 'EndlessPlayerController' is a seperate script
    public DeathMenu m_DeathScreen;

    // Find Vectors
    private Vector3 platformStartPoint;
    private Vector3 playerStartpoint;
    // Find Scripts
    private ObjectDestroyer[] platformList;      // 'ObjectDestroyer' is a seperate script
    private ScoreManager m_ScoreManager;        // 'ScoreManager' is a seperate script

    void Start()
    {
        platformStartPoint = m_LevelGenerator.position;
        playerStartpoint = m_Player.transform.position;
        // Finds Componints on the 'ScoreManager' script
        m_ScoreManager = FindObjectOfType<ScoreManager>();
    }

    public void RestartGame()
    {
        // Changing variables from the ScoreManager script
        m_ScoreManager.canScore = false;
        // Changing variables from the EndlessPlayerController script
        m_Player.gameObject.SetActive(false);
        // Turns on the Death Screen
        m_DeathScreen.gameObject.SetActive(true);

#if UNITY_EDITOR
        PlayerPrefs.DeleteAll();
        Debug.LogWarning("Editor Mode PlayerPrefs Deleted");
#elif !UNITY_EDITOR
        PlayerPrefs.DeleteAll();
        Debug.LogError("Not Editor");
#endif

        //-StartCoroutine("RestartGameCo");
    }

    public void Reset()
    {
        Time.timeScale = 1f;

        // Turns off Death Screen
        m_DeathScreen.gameObject.SetActive(false);

        // Finds Objects from 'ObjectDestroyer' script
        platformList = FindObjectsOfType<ObjectDestroyer>();

        // '.Length' finds the number thePltform is set to automaticly
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        m_Player.transform.position = playerStartpoint;
        m_LevelGenerator.position = platformStartPoint;
        m_Player.gameObject.SetActive(true);
        m_ScoreManager.scoredDistance = 0;    // Changing variables from the ScoreManager script
        m_ScoreManager.canScore = true;
    }


    /*public IEnumerator RestartGameCo()
    {
        // Changing variables from the ScoreManager script
        m_ScoreManager.canScore = false;
        // Changing variables from the EndlessPlayerController script
        m_Player.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        // Finds Objects from 'ObjectDestroyer' script
        platformList = FindObjectsOfType<ObjectDestroyer>();

        // '.Length' finds the number thePltform is set to automaticly
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        m_Player.transform.position = playerStartpoint;
        LevelGenerator.position = platformStartPoint;
        m_Player.gameObject.SetActive(true);
        // Changing variables from the ScoreManager script
        m_ScoreManager.scoreCount = 0;
        m_ScoreManager.canScore = true;
    }*/
}
