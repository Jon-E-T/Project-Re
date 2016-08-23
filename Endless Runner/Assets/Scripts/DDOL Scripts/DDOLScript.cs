using UnityEngine;
using System.Collections;

public class DDOLScript : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType(GetType()).Length > 1f)    // Destroy Duplicates
        {
            Destroy(gameObject);
        }
    }
}
