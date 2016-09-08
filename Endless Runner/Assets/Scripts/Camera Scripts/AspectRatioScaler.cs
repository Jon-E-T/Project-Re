using UnityEngine;
using System.Collections;


public class AspectRatioScaler : MonoBehaviour
{
    private Resolution screenResolution;

    void Start()
    {
        AutoAspect();
    }

    public void AutoAspect()
    {
        Cursor.visible = false;
        // Fits game screen on to most displays
        // 16 : 10 resolution
        //Camera.main.aspect = 5f / 4f;
        //Camera.main.ResetAspect();
        screenResolution = Screen.currentResolution;
        Debug.Log(screenResolution);
        Debug.Log(Camera.main.aspect);
        if (Camera.main.aspect >= 1.7f)
        {
            Camera.main.aspect = 16f / 9f;
            Debug.Log(Camera.main.aspect + "/ 16 : 9");
        }
        else if (Camera.main.aspect >= 1.6f)
        {
            Camera.main.aspect = 16f / 10f;
            Debug.Log(Camera.main.aspect + "/ 16 : 10");
        }
        else if (Camera.main.aspect >= 1.5f)
        {
            Camera.main.aspect = 3f / 2f;
            Debug.Log(Camera.main.aspect + "/ 3:2");
        }
        else if (Camera.main.aspect >= 1.3f)
        {
            Camera.main.aspect = 4f / 3f;
            Debug.Log(Camera.main.aspect + "/ 4:3");
        }
        else if (Camera.main.aspect <= 1.25f)
        {
            Camera.main.aspect = 5f / 4f;
            Debug.Log(Camera.main.aspect + "/ 5:4");
        }

    }
}
