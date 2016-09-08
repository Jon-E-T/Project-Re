using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    public void Awake()
    {
#if (UNITY_IOS || UNITY_ANDROID)
        Application.targetFrameRate = 60;
#else
        Application.targetFrameRate = -1;
#endif
    }
}
