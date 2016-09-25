using UnityEngine;
using System.Collections;

public class EndlessPlayerController : MonoBehaviour
{

    // For Testing 
    public bool godMode;


    // Basic Movment
    public float moveSpeed;
    //public float jumpForce;
    public LayerMask whatIsGround;        // Checks if current object is touching the specified layer
    // Advanced Movment Speed
    public float speedIncreasePointMultiplier = 2;
    public float increaceSpeedEveryX = 50f;      //  Distance untill speed increase
    public float increaseSpeedAmount;
    // Check For Ground
    public Transform m_GroundCheck;         // GameObject that will check for the layer set in 'LayerMask'
    public float groundCheckRadius;
    // Find Scripts
    public GameManager m_GameManager;
    public PlatformManager m_PlatformManager;
    public PlayerJumpController m_PlayerJumpController;
    // Find Objects
    //public GameObject doubleJumpCloudObject;
    // Sounds
    public AudioSource jumpSound;         // Place sound objects in script from editor
    public AudioSource deathSound;        // Place sound objects in script from editor


    // Check For Ground
    [HideInInspector]
    public bool grounded;
    // Find Components
    [HideInInspector]
    public Rigidbody2D myRigidbody;
    // Original Variables
    [HideInInspector]
    public float originalMoveSpeed;


    // Find Scripts
    private ScoreManager m_ScoreManager;
    private LevelGenerator m_LevelGenerator;
    // Find Components
    private Animator myAnimator;
    // Advanced Move Speed
    private float speedIncreasePoint;
    private float platformIncreaseDistance;
    // Original Variables
    //Player
    private float originalSpeedIncreasePoint;
    // Platform
    private float originalPlatformMax;
    private float originalPlatformMin;


    public void Awake()
    {
        // Find Components
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        // Set Variables
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
        // PlayerJumpController
        m_PlayerJumpController.PlayerJump();
        m_PlayerJumpController.DownForceMethod();
        m_PlayerJumpController.PlayerDoubleJumpCloud();
        // Animations
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
            m_PlayerJumpController.downForceCountDown = m_PlayerJumpController.downForceActiveTime;
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
