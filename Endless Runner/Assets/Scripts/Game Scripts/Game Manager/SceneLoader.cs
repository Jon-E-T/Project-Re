using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSingleScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
