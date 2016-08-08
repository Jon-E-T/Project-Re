using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{
    // Drag GameObjects from editor
    public GameObject pauseScreen;
    public GameObject deathScreen;
    public GameObject pauseButtonToggle;

    void Start()
    {

    }

    void Update()
    {
        PauseOnEscapePress();

        OnScreenPauseButtonToggle();

        MouseCursorToggle();
    }

    void OnScreenPauseButtonToggle()
    {
        if(pauseScreen.activeInHierarchy || deathScreen.activeInHierarchy)
        {
            pauseButtonToggle.GetComponent<Button>().interactable = false;
        }
        else
        {
            pauseButtonToggle.GetComponent<Button>().interactable = true;
        }
    }

    void PauseOnEscapePress()
    {
        // Checks if Escape was pressed and if the GameObject we selectied above is active or not
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseScreen.activeInHierarchy && !deathScreen.activeInHierarchy)
        {
            FindObjectOfType<PauseMenu>().PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseScreen.activeInHierarchy)
        {
            FindObjectOfType<PauseMenu>().ResumeGame();
        }
    }

    void MouseCursorToggle()
    {
        OnScreenPauseButtonToggle();
        if (Time.timeScale == 0)
        {
            Cursor.visible = true;
        }
        else if (Time.timeScale > 0)
        {
            Cursor.visible = false;
        }
    }

}
