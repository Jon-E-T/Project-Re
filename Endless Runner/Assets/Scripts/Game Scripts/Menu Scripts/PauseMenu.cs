using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    // Find GameObjests
    public GameObject pauseMenuScreen;
    // Find Scripts
    public GameManager m_GameManager;
    public GameTimeScale m_GameTimeScale;

    public void PauseGame()
    {
        m_GameTimeScale.GameTimeScaleSet(0);    // Game Speed = 0
        pauseMenuScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseMenuScreen.SetActive(false);
        m_GameTimeScale.GameTimeScaleSet(1);    // Game Speed = 1
    }

    public void RestartGame()
    {
        pauseMenuScreen.SetActive(false);
        m_GameTimeScale.GameTimeScaleSet(1);    // Game Speed = 1
        m_GameManager.RestartGame();
    }
}
