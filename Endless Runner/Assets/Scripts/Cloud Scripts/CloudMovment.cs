using UnityEngine;
using System.Collections;

public class CloudMovment : MonoBehaviour
{
    public float cloudMovmentSpeed;


    // Find Components
    private Rigidbody2D myRigidbody2D;

    // Use this for initialization
    void Start()
    {
        // Find Components
        myRigidbody2D = GetComponent<Rigidbody2D>();

        // Changes Variables
        cloudMovmentSpeed -= (cloudMovmentSpeed * 2);    // Changes 'CloudMovmentSpeed' to negative (Moves Cloud Left)
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myRigidbody2D.velocity = new Vector2(cloudMovmentSpeed, myRigidbody2D.velocity.y);
    }
}
