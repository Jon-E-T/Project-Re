using UnityEngine;
using System.Collections;

public class ObjectDestroyer : MonoBehaviour
{
    public GameObject destructionPoint;

    void Start()
    {
        destructionPoint = GameObject.Find("Platform Destruction Point");
    }

    void Update()
    {
        if(transform.position.x < destructionPoint.transform.position.x)
        {
            //-Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
