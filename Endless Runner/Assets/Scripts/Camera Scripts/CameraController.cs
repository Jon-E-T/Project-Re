using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour
{

    public EndlessPlayerController thePlayer;

    private Vector3 lastPlayerPosition;
    private float distianceToMove;
	void Start ()
    {
        thePlayer = FindObjectOfType<EndlessPlayerController>();
        lastPlayerPosition = thePlayer.transform.position;

    }

    void Update ()
    {
        distianceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;

        transform.position = new Vector3(transform.position.x + distianceToMove, transform.position.y, transform.position.z);

        lastPlayerPosition = thePlayer.transform.position;
    }
}
