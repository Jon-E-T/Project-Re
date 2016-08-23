using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    public void ResetPlayerHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", 0);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
