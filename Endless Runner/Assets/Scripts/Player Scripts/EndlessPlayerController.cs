using UnityEngine;
using System.Collections;

public class EndlessPlayerController : MonoBehaviour {

    // For Testing 
    public bool godMode;


    public float moveSpeed;
    public float jumpForce;
    public bool grounded;
    // Checks if current object is touching the specified layer
    public LayerMask whatIsGround;
    public float jumpTime;
    public bool canDoubleJump;
    public float speedMultiplier;
    //  Distance untill speed increase
    public float speedIncreasePoint;
    // GameObject that will check for the layer set in 'LayerMask'
    public Transform groundCheck;
    public float groundCheckRadius;
    // Uses the scripts from the Game Object "GameManager"
    public GameManager theGameManager;
    // Place sound objects in script from editor
    public AudioSource jumpSound;
    public AudioSource deathSound;


    private Rigidbody2D myRigidbody;
    //-private Collider2D myCollider;
    private Animator myAnimator;
    private float jumpTimeCounter;
    private bool midJump;
    private float speedPointCount;
    private float moveSpeedStore;
    private float moveSpeedPointCountStore;
    private float speedIncreasePointStore;

    void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        //-myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        // Stops Plyer from jumping when falling
        midJump = true;
        speedPointCount = speedIncreasePoint;
        moveSpeedStore = moveSpeed;
        moveSpeedPointCountStore = speedPointCount;
        speedIncreasePointStore = speedIncreasePoint;
    }
	
	void Update ()
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
        //                                             "_______.velocity.y" dosent Change y and Leaves y the same as it was
        myRigidbody.velocity = new Vector2(moveSpeed , myRigidbody.velocity.y);

        PlayerJump();

        // Sets the Players Animaton conditions
        myAnimator.SetFloat("Speed" , myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded" , grounded);
        myAnimator.SetFloat("Fall" , myRigidbody.velocity.y);
    }

    // other is the name for any collision 
    void OnCollisionEnter2D(Collision2D other) // Player death
    {
        // Checks if this GameObject is Colliding with a gameObject with the tag "Killbox"
        if (other.gameObject.tag == "Killbox")
        {
            Time.timeScale = 0f;
            
            deathSound.Play();
            // theGameManager is using the GameManager script
            // RestartGame() is a class in GameManager script
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
                // Stops Plyer from jumping when falling
                midJump = false;
                jumpSound.Play();
            }

            // Dubble Jump
            if (!grounded && canDoubleJump)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                // Hight of Dubble Jump
                jumpTimeCounter = jumpTime / 2;
                midJump = false;
                canDoubleJump = false;
                jumpSound.Play();
            }
        }

        // Hold For Higher Jump
        if ( (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0) ) && !midJump)
        {
            if (jumpTimeCounter > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                // Stops player frome jumping forever
                jumpTimeCounter -= Time.deltaTime;
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
