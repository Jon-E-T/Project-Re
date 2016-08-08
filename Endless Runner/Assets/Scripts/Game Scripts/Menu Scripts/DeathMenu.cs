using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    // Death Menu Buttons


    public void Restart()
    {
        // Finds the script 'GameManager' then runs its method 'Reset'
        FindObjectOfType<GameManager>().Reset();
    }

    public void QuitToMain()
    {
        Time.timeScale = 1f;

        // I can also use a Varriable instad of a literal "__" String
        // the Variable can ba changed and updated at any time unlike a literal "___" string
        SceneManager.LoadScene("Main Menu");
    }
}