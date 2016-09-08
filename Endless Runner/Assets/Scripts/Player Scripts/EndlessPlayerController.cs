using UnityEngine;
using System.Collections;

public class EndlessPlayerController : MonoBehaviour
{

    // For Testing 
    public bool godMode;


    // Basic Movment
    public float moveSpeed;
    public float jumpForce;
    public LayerMask whatIsGround;        // Checks if current object is touching the specified layer
    // Advanced Jumping
    public float jumpTime;
    public bool canDoubleJump;
    public float doubleJumpCloudTime;
    // Advanced Movment Speed
    public float speedIncreasePointMultiplier = 2;
    public float increaceSpeedEveryX = 50f;      //  Distance untill speed increase
    public float increaseSpeedAmount;
    // Check For Ground
    [HideInInspector] public bool grounded;
    public Transform m_GroundCheck;         // GameObject that will check for the layer set in 'LayerMask'
    public float groundCheckRadius;
    // Find Scripts
    public GameManager m_GameManager;
    // Find Objects
    public GameObject doubleJumpCloudObject;
    // Sounds
    public AudioSource jumpSound;         // Place sound objects in script from editor
    public AudioSource deathSound;        // Place sound objects in script from editor


    // Find Scripts
    private ScoreManager m_ScoreManager;
    private LevelGenerator m_LevelGenerator;
    // Find Components
    private Animator myAnimator;
    private Rigidbody2D myRigidbody;
    // Advanced Jumping
    private float jumpTimeCounter;
    private bool midJump;
    private bool doubleJumpCloudEnabled;
    private float doubleJumpCloudTimeCounter;
    // Advanced Move Speed
    private float speedIncreasePoint;
    private float platformIncreaseDistance;
    // Original Variables
    //Player
    [HideInInspector] public float originalMoveSpeed;
    private float originalSpeedIncreasePoint;
    // Platform
    private float originalPlatformMax;
    private float originalPlatformMin;

    // Awake is called when the script instance is being loaded
    public void Awake()
    {
        // Find Components
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        // Set Variables
        midJump = true;    // Stops Plyer from jumping when falling
        jumpTimeCounter = jumpTime;
        doubleJumpCloudTimeCounter = doubleJumpCloudTime;
        speedIncreasePoint = increaceSpeedEveryX;
        platformIncreaseDistance = increaseSpeedAmount;

        // Find Original Variables
        originalMoveSpeed = moveSpeed;
        originalSpeedIncreasePoint = speedIncreasePoint;

    }

    void Start()
    {
        // Find Scripts
        m_LevelGenerator = FindObjectOfType<LevelGenerator>();
        m_ScoreManager = FindObjectOfType<ScoreManager>();

        // Find Object

        // Set Variables
        originalPlatformMax = m_LevelGenerator.distanceBetweenPlatformsMax;
        originalPlatformMin = m_LevelGenerator.distanceBetweenPlatformsMin;

    }

    void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(m_GroundCheck.position, groundCheckRadius, whatIsGround);

        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);    // "_______.velocity.y" Dosent Change y And Leaves y The Same As It Was
    }

    public void Update()
    {
        GodModeActive();
        SpeedIncreace();
        PlayerJump();
        PlayerDoubleJumpCloud();
        AnimationConditions();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Player death
        if (other.gameObject.tag == "Killbox")    // Checks if this GameObject is Colliding with a gameObject with the tag "Killbox"
        {
            Time.timeScale = 0f;

            // Sounds 
            deathSound.Play();


            // 'm_GameManager' Is Using The 'GameManager' Script
            // 'RestartGame()' Is A Method In 'GameManager' Script
            m_GameManager.RestartGame();

            // Restore Original Variables
            moveSpeed = originalMoveSpeed;
            speedIncreasePoint = originalSpeedIncreasePoint;
            m_LevelGenerator.distanceBetweenPlatformsMax = originalPlatformMax;
            m_LevelGenerator.distanceBetweenPlatformsMin = originalPlatformMin;
        }
    }

    void AnimationConditions()    // Sets the Players Animaton conditions
    {
        // Sets the Players Animaton conditions
        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
        myAnimator.SetFloat("Fall", myRigidbody.velocity.y);
    }

    void SpeedIncreace()
    {
        if (m_ScoreManager.scoredDistance >= speedIncreasePoint)
        {
            // Player Speed Increase
            speedIncreasePoint = (Mathf.RoundToInt(m_ScoreManager.scoredDistance) + increaceSpeedEveryX) * speedIncreasePointMultiplier;
            moveSpeed += increaseSpeedAmount;

            // Platform Increase
            m_LevelGenerator.distanceBetweenPlatformsMin += platformIncreaseDistance;
            m_LevelGenerator.distanceBetweenPlatformsMax += platformIncreaseDistance;

            // Spawn Power-Ups
            m_LevelGenerator.PowerUpSpawn();
        }
    }

    void PlayerJump()
    {
        // Player Jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // First Jump
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                midJump = false;    // Stops Plyer from jumping when falling

                // Sounds
                jumpSound.Play();
            }


            // Dubble Jump
            if (!grounded && canDoubleJump)
            {
                // Player Changes
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime / 2;    // Hight of Dubble Jump

                // Change bools
                midJump = false;
                canDoubleJump = false;
                doubleJumpCloudEnabled = true;

                // Sounds
                jumpSound.Play();
            }
        }


        // Hold For Higher Jump
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !midJump)
        {
            if (jumpTimeCounter > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;    // Stops player frome jumping forever

            }
        }


        // Jump Stops When Jump Button Is Relesed
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
            midJump = true;
        }


        // Resets The 'jumpTimeCounter'
        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
            doubleJumpCloudTimeCounter = doubleJumpCloudTime;
        }

    }

    void PlayerDoubleJumpCloud()
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

    public void MovePlayerToTop()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_LevelGenerator.m_TopOfScreenRight.transform.position, moveSpeed * Time.deltaTime);    // Moves Player To Top Of The Screen
    }

    // FOR TESTING ONLY 
    void GodModeActive()
    {
        if (godMode)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().freezeRotation = true;
        }
    }
}
