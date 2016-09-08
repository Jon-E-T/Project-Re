using UnityEngine;
using System.Collections;

public class ObjectDestroyer : MonoBehaviour
{
    private GameObject destructionPoint;

    void Start()
    {
        destructionPoint = GameObject.Find("Platform Destruction Point");
    }

    void Update()
    {
        if (transform.position.x < destructionPoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }
    }
}
