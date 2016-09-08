using UnityEngine;
using System.Collections;

public class CountdownManager : MonoBehaviour
{

    public bool countdownOver = false;    // is CountdownOver
    // Find Scripts
    public EndlessPlayerController m_EndlessPlayerController;


    public void Start()
    {
        // Set Variables
        m_EndlessPlayerController.moveSpeed = 0;    // Player Speed = 0
    }

    public void SetCountDown()
    {
        countdownOver = true;
        gameObject.SetActive(false);    // Turns Off Countdown GameObject
        // Set Variables
        m_EndlessPlayerController.moveSpeed = m_EndlessPlayerController.originalMoveSpeed;    // Player Speed = 'originalMoveSpeed'
    }

}
