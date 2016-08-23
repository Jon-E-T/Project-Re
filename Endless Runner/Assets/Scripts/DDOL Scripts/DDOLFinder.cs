using UnityEngine;
using System.Collections;

public class DDOLFinder : MonoBehaviour
{
    private GameObject ddolFind;
    private DDOLScript theDDOLScript;

    public void Awake()
    {
        // Find Objects
        ddolFind = GameObject.Find("DDOL");

        // Find Scripts
        theDDOLScript = FindObjectOfType<DDOLScript>();
    }
}
