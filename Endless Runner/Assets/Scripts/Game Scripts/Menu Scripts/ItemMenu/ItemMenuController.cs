using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ItemMenuController : MonoBehaviour
{

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
