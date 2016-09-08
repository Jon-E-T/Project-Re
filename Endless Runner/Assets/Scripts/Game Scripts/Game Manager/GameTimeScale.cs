using UnityEngine;
using System.Collections;

public class GameTimeScale : MonoBehaviour
{
    // Time Scale
    public void GameTimeScaleSet(int gameSpeed)
    {
        Time.timeScale = gameSpeed;
    }
}
