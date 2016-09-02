using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Follow Player Camera 
    /// Or  Selected Object
    /// </summary>

    // ReRunner Size 10 / FOV 90
    
    // Rename To Script With Object I Want To Follow
    public EndlessPlayerController m_Player;

    private Vector3 lastPlayerPosition;
    private float distianceToMove;
    private float distanceToMoveUp;

    void Start()
    {
        CameraFollowObjectSelector();
    }

    void LateUpdate()
    {
        CameraFollow();
    }

    void CameraFollowObjectSelector()
    {
        //m_Player = FindObjectOfType<EndlessPlayerController>();
        lastPlayerPosition = m_Player.transform.position;
    }

    void CameraFollow()
    {
        // Follows X Axis Disable To Stop Following X
        distianceToMove = m_Player.transform.position.x - lastPlayerPosition.x;

        // Follows Y Axis Disable To Stop Following Y
        distanceToMoveUp = m_Player.transform.position.y - lastPlayerPosition.y;

        if (m_Player.transform.position.y > 7.5 || (m_Player.transform.position.y < -4 && m_Player.transform.position.y > -6))
        {
            MoveCameraYAxis();
        }
        else if (m_Player.transform.position.y < 7.5)
        {
            MoveCameraXAxis();
        }

        lastPlayerPosition = m_Player.transform.position;
    }

    // Moves Camera X And Y Axis
    void MoveCameraYAxis()
    {
        // Moves Camera To New Position
        transform.position = new Vector3(transform.position.x + distianceToMove, transform.position.y + distanceToMoveUp, transform.position.z);
    }

    // Moves Camera X Axis Only
    void MoveCameraXAxis()
    {
        //-transform.position = new Vector3(transform.position.x + distianceToMove, transform.position.y, transform.position.z);

        // Moves Camera To New Position
        transform.position = new Vector3(transform.position.x + distianceToMove, 2, transform.position.z);
    }
}
