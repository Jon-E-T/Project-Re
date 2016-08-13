using UnityEngine;
using System.Collections;

public class EndlessPlayerController : MonoBehaviour
{

    // For Testing 
    public bool godMode;

    // Basic Movment
    public float moveSpeed;
    public float jumpForce;
    public bool grounded;
    public LayerMask whatIsGround;        // Checks if current object is touching the specified layer
    // Advanced Jumping
    public float jumpTime;
    public bool canDoubleJump;
    // Advanced Movment Speed
    public float speedMultiplier;
    public float speedIncreasePoint;      //  Distance untill speed increase
    // Check For Ground
    public Transform groundCheck;         // GameObject that will check for the layer set in 'LayerMask'
    public float groundCheckRadius;
    public GameManager theGameManager;    // Uses the scripts from the Game Object "GameManager"
    // Sounds
    public AudioSource jumpSound;         // Place sound objects in script from editor
    public AudioSource deathSound;        // Place sound objects in script from editor


    private Rigidbody2D myRigidbody;
    //-private Collider2D myCollider;
    private Animator myAnimator;
    // Advanced Jumping
    private float jumpTimeCounter;
    private bool midJump;
    // Advanced Movment Speed
    private float speedPointCount;
    private float moveSpeedStore;
    private float moveSpeedPointCountStore;
    private float speedIncreasePointStore;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        //-myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        midJump = true;        // Stops Plyer from jumping when falling
        speedPointCount = speedIncreasePoint;
        moveSpeedStore = moveSpeed;
        moveSpeedPointCountStore = speedPointCount;
        speedIncreasePointStore = speedIncreasePoint;
    }

    void Update()
    {
        GodModeActive();

        // 'grounded' = true if the layer with 'myCollider' and 'whatIsGround' are touching
        //-grounded = Physics2D.IsTouchingLayers(myCollider , whatIsGround);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // speed up at points over time
        if (transform.position.x > speedPointCount)
        {
            speedPointCount += speedIncreasePoint;
            speedIncreasePoint = speedIncreasePoint * speedMultiplier;
            moveSpeed = moveSpeed * speedMultiplier;
        }
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);        // "_______.velocity.y" Dosent Change y And Leaves y The Same As It Was

        PlayerJump();

        // Sets the Players Animaton conditions
        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
        myAnimator.SetFloat("Fall", myRigidbody.velocity.y);
    }

    // other is the name for any collision 
    void OnCollisionEnter2D(Collision2D other) // Player death
    {
        // Checks if this GameObject is Colliding with a gameObject with the tag "Killbox"
        if (other.gameObject.tag == "Killbox")
        {
            Time.timeScale = 0f;

            deathSound.Play();
            // 'theGameManager' Is Using The 'GameManager' Script
            // 'RestartGame()' Is A Method In 'GameManager' Script
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedPointCount = moveSpeedPointCountStore;
            speedIncreasePoint = speedIncreasePointStore;
        }
    }

    void PlayerJump()
    {
        // Start Jump
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
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime / 2;    // Hight of Dubble Jump

                midJump = false;
                canDoubleJump = false;
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
        }

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
