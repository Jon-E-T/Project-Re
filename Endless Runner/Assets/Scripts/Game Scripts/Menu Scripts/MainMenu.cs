using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Find Animation
    public Animator m_MenuSlideAnimation;
    // Find GameObjects
    public GameObject m_MenuButton;
    // Find Scripts
    public SceneLoader m_SceneLoader;
    public GameTimeScale m_GameTimeScale;


    private bool slideMenuActive = false;

    // TEST
    bool fps60 = true;
    public Text testText;


    public void Awake()
    {
        m_GameTimeScale.GameTimeScaleSet(1);
        m_MenuButton.GetComponent<Animator>().enabled = true;    // Enables Menu Button Animator "Stops Warning"
        Cursor.visible = true;
    }

    public void StartGame()
    {
        if (slideMenuActive)    // if 'slideMenuActive' = true Then 'StartGame' Turns into 'SlidingMenuButton' (To Close The Sliding Menu)
        {
            SlidingMenuButton();
        }
        else    //  if 'slideMenuActive' = false Then 'StartGame'
        {
            m_SceneLoader.LoadSingleScene("Endless Main");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SlidingMenuButton()
    {
        // Find Bool
        bool isHidden = m_MenuSlideAnimation.GetBool("isHidden");

        // Set Bool
        slideMenuActive = !slideMenuActive;
        // Animation
        m_MenuSlideAnimation.SetBool("isHidden", !isHidden);    // Sets Bool  To What Its Not
    }

    public void ResetPlayerHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", 0);
        PlayerPrefs.SetInt("CoinScore", 0);
    }

    // TEST
    public void FPSToggle()
    {
        fps60 = !fps60;
        if (!fps60)
        {
            Application.targetFrameRate = 30;
            testText.text = "Low";
        }
        else
        {
            Application.targetFrameRate = 60;
            testText.text = "High";
        }
    }
}
