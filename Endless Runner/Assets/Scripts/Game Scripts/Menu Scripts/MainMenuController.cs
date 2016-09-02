using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Find Animation
    public Animator m_MenuSlideAnimation;


    private bool slideMenuActive = false;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = true;
    }

    public void StartGame()
    {
        if (slideMenuActive)
        {
            SlideMenu();
        }
        else
        {
            SceneManager.LoadScene("Endless Main");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene("Settings");
    }

    public void SlideMenu()
    {
        // Find Bool
        bool isHidden = m_MenuSlideAnimation.GetBool("isHidden");

        // Set Bool
        slideMenuActive = !slideMenuActive;
            // Animation
        m_MenuSlideAnimation.SetBool("isHidden", !isHidden);    // Sets Bool  To What Its Not
    }

    public void ItemScreen()
    {
        SceneManager.LoadScene("Item Options");
    }
}
