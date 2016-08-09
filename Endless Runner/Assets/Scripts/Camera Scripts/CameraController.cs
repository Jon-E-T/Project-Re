using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Follow Player Camera 
    /// Or  Selected Object
    /// </summary>

    // Rename To Script With Object I Want To Follow
    public EndlessPlayerController thePlayer;

    private Vector3 lastPlayerPosition;
    private float distianceToMove;
    private float distanceToMoveUp;

	void Start ()
    {
        CameraFollowObjectSelector();
    }

    void Update ()
    {
        CameraFollow();
    }

    void CameraFollowObjectSelector()
    {
        thePlayer = FindObjectOfType<EndlessPlayerController>();
        lastPlayerPosition = thePlayer.transform.position;
    }

    void CameraFollow()
    {
        // Follows X Axis Disable To Stop Following X
        distianceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;

        // Follows Y Axis Disable To Stop Following Y
        distanceToMoveUp = thePlayer.transform.position.y - lastPlayerPosition.y;

        if(thePlayer.transform.position.y > 7.5)
        {
            MoveCameraYAxis();
        }
        else if(thePlayer.transform.position.y < 7.5)
        {
            MoveCameraXAxis();
        }

        lastPlayerPosition = thePlayer.transform.position;
    }

    // Moves Camera X And Y Axis
    void MoveCameraYAxis()
    {
        // Moves Camera To New Position
        transform.position = new Vector3(transform.position.x + distianceToMove, transform.position.y + distanceToMoveUp, transform.position.z);
    }

    // Moves Camera X Axis Only
void MoveCameraXAxis()
    {        // Moves Camera To New Position
        transform.position = new Vector3(transform.position.x + distianceToMove, transform.position.y, transform.position.z);

    }
}
