using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    // Death Menu Buttons


    public void Restart()
    {
        // Finds the script 'GameManager' then runs its method 'Reset'
        FindObjectOfType<GameManager>().Reset();
    }

}