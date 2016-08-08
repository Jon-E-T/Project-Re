using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Finds the GameObject Thats is set with the Variable
    public GameObject pauseMenuScreen;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseMenuScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        pauseMenuScreen.SetActive(false);
        Time.timeScale = 1f;
        // Finds the script 'GameManager' then runs its method 'Reset'
        FindObjectOfType<GameManager>().RestartGame();
    }

    public void QuitToMain()
    {
        Time.timeScale = 1f;
        // I can also use a Varriable instad of a literal "__" String
        // the Variable can ba changed and updated at any time unlike a literal "___" string
        SceneManager.LoadScene("Main Menu");
    }
}
