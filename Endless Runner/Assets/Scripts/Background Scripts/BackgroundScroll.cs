using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour
{

    public float backgroundSpeed = 0.1f;

    void Start()
    {

    }

    void Update()
    {
        Vector2 offset = new Vector2(Time.time * backgroundSpeed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;

    }
}
