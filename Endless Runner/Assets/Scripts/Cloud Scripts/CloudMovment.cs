using UnityEngine;
using System.Collections;

public class CloudMovment : MonoBehaviour
{
    public float cloudMovmentSpeed;


    void Start()
    {

    }

    // This function is called when the object becomes enabled and active
    public void OnEnable()
    {
        // Selects Cloud Movment Direction
        switch (Random.Range(0, 1))
        {
            case 0:
                cloudMovmentSpeed -= (cloudMovmentSpeed * 2);    // 'CloudMovmentSpeed' Set To Negative
                break;
            case 1:
                cloudMovmentSpeed += (cloudMovmentSpeed * 2);    // 'CloudMovmentSpeed' Set To Positive
                break; 
        }
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.left * cloudMovmentSpeed * Time.deltaTime);
    }
}
