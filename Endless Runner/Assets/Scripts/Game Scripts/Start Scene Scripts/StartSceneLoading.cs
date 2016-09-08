using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneLoading : MonoBehaviour
{
    // Loading
    public GameObject loadingImage;
    public Slider loadingBar;


    // Async Loading
    private AsyncOperation asyncLoading;

    public void Start()
    {
        LoadSceneWithBar("Main Menu");
    }

    void LoadSceneWithBar(string sceneName)
    {
        loadingImage.SetActive(true);    // Activates GameObject
        StartCoroutine(loadBarProgress(sceneName));    // Starts Coroutien
    }

    IEnumerator loadBarProgress(string sceneName)
    {
        asyncLoading = SceneManager.LoadSceneAsync(sceneName);
        // Moves Loading Bar
        while (!asyncLoading.isDone)
        {
            loadingBar.value = asyncLoading.progress;
            yield return null;
        }
    }
}
