using UnityEngine;
using System.Collections;

public class MainButtonManager : MonoBehaviour
{
    // Find Scripts
    public PlayerJumpController m_PlayerJumpController;


    // On Game Screen Buttons


    // Screen Pressed
    public void ScreenPressed()
    {
        m_PlayerJumpController.jumpActive = true;
    }

    // Screen Not Pressed
    public void ScreenNotPressed()
    {
        m_PlayerJumpController.jumpActive = false;
    }


    // Down Force Button
    public void DownForceActive()
    {
        m_PlayerJumpController.downForceActive = true;
    }

    public void DownForceNotActive()
    {
        m_PlayerJumpController.downForceActive = false;
    }

}
