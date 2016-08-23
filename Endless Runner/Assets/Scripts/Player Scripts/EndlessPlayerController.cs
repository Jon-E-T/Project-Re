using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    public GameObject doubleJumpCloudObject;
    public float doubleJumpCloudTime;
    // Advanced Movment Speed
    public Transform topOfScreen;         // Find The Top Of The Screen        
    public float speedIncreasePointMultiplier = 2;
    public float increaceSpeedEveryX = 50f;      //  Distance untill speed increase
    public float increaseSpeedAmount;
    // Check For Ground
    public bool grounded;
    public Transform groundCheck;         // GameObject that will check for the layer set in 'LayerMask'
    public float groundCheckRadius;
    public GameManager theGameManager;    // Uses the scripts from the Game Object "GameManager"
    // Sounds
    public AudioSource jumpSound;         // Place sound objects in script from editor
    public AudioSource deathSound;        // Place sound objects in script from editor


    private ScoreManager theScoreManager;
    private Rigidbody2D myRigidbody;
    private LevelGenerator theLevelGenerator;
    private Animator myAnimator;
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
    private float originalMoveSpeed;
    private float originalSpeedIncreasePoint;
        // Platform
    private float originalPlatformMax;
    private float originalPlatformMin;


    void Start()
    {
        // Find Scripts
        theLevelGenerator = FindObjectOfType<LevelGenerator>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        // Find Object

        // Set Variables
        midJump = true;    // Stops Plyer from jumping when falling
        jumpTimeCounter = jumpTime;
        doubleJumpCloudTimeCounter = doubleJumpCloudTime;
        speedIncreasePoint = increaceSpeedEveryX;
        platformIncreaseDistance = increaseSpeedAmount;// -0.1f;

        // Find Original Variables
        originalMoveSpeed = moveSpeed;
        originalSpeedIncreasePoint = speedIncreasePoint;
        originalPlatformMax = theLevelGenerator.distanceBetweenPlatformsMax;
        originalPlatformMin = theLevelGenerator.distanceBetweenPlatformsMin;

    }

    void Update()
    {
        GodModeActive();

        SpeedIncreace();

        // 'grounded' = true if the layer with 'myCollider' and 'whatIsGround' are touching
        //-grounded = Physics2D.IsTouchingLayers(myCollider , whatIsGround);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);


        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);    // "_______.velocity.y" Dosent Change y And Leaves y The Same As It Was

        PlayerJump();

        PlayerDoubleJumpCloud();

        AnimationConditions();
    }

    // other is the name for any collision 
    void OnCollisionEnter2D(Collision2D other)
    {
        // Player death
        if (other.gameObject.tag == "Killbox")    // Checks if this GameObject is Colliding with a gameObject with the tag "Killbox"
        {
            Time.timeScale = 0f;

            deathSound.Play();
            // 'theGameManager' Is Using The 'GameManager' Script
            // 'RestartGame()' Is A Method In 'GameManager' Script
            theGameManager.RestartGame();

            moveSpeed = originalMoveSpeed;
            speedIncreasePoint = originalSpeedIncreasePoint;
            theLevelGenerator.distanceBetweenPlatformsMax = originalPlatformMax;
            theLevelGenerator.distanceBetweenPlatformsMin = originalPlatformMin;
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
        if (theScoreManager.scoredDistance > speedIncreasePoint)
        {
            // Player Speed Increase
            speedIncreasePoint = Mathf.RoundToInt(theScoreManager.scoredDistance) * speedIncreasePointMultiplier + increaceSpeedEveryX;
            moveSpeed += increaseSpeedAmount;

            // Platform Increase
            theLevelGenerator.distanceBetweenPlatformsMin += platformIncreaseDistance;
            theLevelGenerator.distanceBetweenPlatformsMax += platformIncreaseDistance;

            // Spawn Power-Ups
            theLevelGenerator.PowerUpSpawn();
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
        transform.position = Vector3.MoveTowards(transform.position, topOfScreen.transform.position, moveSpeed * Time.deltaTime);    // Moves Player To Top Of The Screen
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
