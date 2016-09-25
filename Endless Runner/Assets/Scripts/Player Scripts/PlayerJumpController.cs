using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerJumpController : MonoBehaviour
{
    // Find Scripts
    public EndlessPlayerController m_EndlessPlayerController;
    // Jumping Values
    public float jumpForce;
    public float jumpTime;
    // Down Force
    public float downForce;
    public float downForceActiveTime;
    // Find Game Objects
    public Slider downForceCountDownBar;
    // Double Jump Cloud
    public float doubleJumpCloudTime;
    // Find Game Objects
    public GameObject doubleJumpCloudObject;


    // Jump
    [HideInInspector]
    public bool canDoubleJump;
    [HideInInspector]
    public bool jumpActive;
    // Down Force
    [HideInInspector]
    public bool downForceActive;
    [HideInInspector]
    public float downForceCountDown;
    // Jump
    private bool midJump;
    private float jumpTimeCounter;
    // Down Force
    // Double Jump Cloud
    private bool doubleJumpCloudEnabled;
    private float doubleJumpCloudTimeCounter;


    public void Awake()
    {
        // Jump
        midJump = true;    // Stops Plyer from jumping when falling
        jumpTimeCounter = jumpTime;
        // Down Force
        downForceCountDown = downForceActiveTime;
        // Double Jump Cloud
        doubleJumpCloudTimeCounter = doubleJumpCloudTime;
    }

    public void Start()
    {
        // Set Variables
        // Sets 'downForceCountDownBar' Slider's Max Value 
        downForceCountDownBar.maxValue = downForceCountDown;
    }

    public void Update()
    {
        // Counts Down The 'downForceCountDownBar' Slider
        downForceCountDownBar.value = downForceCountDown;
    }

    public void PlayerJump()
    {
        // Player Jump
        if (Input.GetButtonDown("Jump") && jumpActive)
        {
            // First Jump
            if (m_EndlessPlayerController.grounded)
            {
                m_EndlessPlayerController.myRigidbody.velocity = new Vector2(m_EndlessPlayerController.myRigidbody.velocity.x, jumpForce);
                midJump = false;    // Stops Plyer from jumping when falling

                // Sounds
                m_EndlessPlayerController.jumpSound.Play();
            }


            // Dubble Jump
            if (!m_EndlessPlayerController.grounded && canDoubleJump)
            {
                // Player Changes
                m_EndlessPlayerController.myRigidbody.velocity = new Vector2(m_EndlessPlayerController.myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime / 2;    // Hight of Dubble Jump

                // Change bools
                midJump = false;
                canDoubleJump = false;
                doubleJumpCloudEnabled = true;

                // Sounds
                m_EndlessPlayerController.jumpSound.Play();
            }
        }


        // Hold For Higher Jump
        if (Input.GetButton("Jump") && jumpActive && !midJump)
        {
            if (jumpTimeCounter > 0)
            {
                m_EndlessPlayerController.myRigidbody.velocity = new Vector2(m_EndlessPlayerController.myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;    // Stops player frome jumping forever

            }
        }


        // Jump Stops When Jump Button Is Relesed
        if (Input.GetButtonUp("Jump") && !jumpActive)
        {
            jumpTimeCounter = 0;
            midJump = true;
        }


        // Resets The 'jumpTimeCounter'
        if (m_EndlessPlayerController.grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
            doubleJumpCloudTimeCounter = doubleJumpCloudTime;
        }

    }

    public void DownForceMethod()
    {
        if (Input.GetButton("Down Force") && downForceActive)
        {
            // Count Down By Seconds
            downForceCountDown -= Time.deltaTime;

            if (downForceCountDown > 0) // Activate Down Force
            {
                m_EndlessPlayerController.myRigidbody.velocity = new Vector2(m_EndlessPlayerController.moveSpeed, downForce);
            }
            else if (downForceCountDown <= 0) // Sets 'downForceCountDown' To 0
            {
                downForceCountDown = 0;
            }
        }
    }

    public void PlayerDoubleJumpCloud()
    {
        if (doubleJumpCloudEnabled)
        {
            if (doubleJumpCloudTimeCounter > 0)
            {
                doubleJumpCloudObject.SetActive(true);

                doubleJumpCloudTimeCounter -= Time.deltaTime;
            }
            else if (doubleJumpCloudTimeCounter <= 0)
            {
                doubleJumpCloudObject.SetActive(false);
                doubleJumpCloudEnabled = false;
            }

        }
    }

}
