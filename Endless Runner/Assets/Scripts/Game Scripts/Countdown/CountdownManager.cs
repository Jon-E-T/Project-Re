using UnityEngine;
using System.Collections;

public class CountdownManager : MonoBehaviour
{

    public bool countdownActive = false;
    public EndlessPlayerController m_EndlessPlayerController;


    private float originalPlayerMoveSpeed;

    public void Awake()
    {
        originalPlayerMoveSpeed = m_EndlessPlayerController.moveSpeed;
    }

    public void Start()
    {
        m_EndlessPlayerController.moveSpeed = 0;
    }

    public void SetCountDown()
    {
        countdownActive = true;
        gameObject.SetActive(false);
        m_EndlessPlayerController.moveSpeed = originalPlayerMoveSpeed;
    }

}
