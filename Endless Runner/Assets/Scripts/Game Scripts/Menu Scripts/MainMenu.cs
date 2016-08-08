using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Cursor.visible = false;
        // I can also use a Varriable instad of a literal "__" String
        // the Variable can ba changed and updated at any time unlike a literal "___" string
        SceneManager.LoadScene("Endless Main");
    }

    public void Settings()
    {
        // I can also use a Varriable instad of a literal "__" String
        // the Variable can ba changed and updated at any time unlike a literal "___" string
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame()
    {
        // Quits game
        Application.Quit();
    }
}
