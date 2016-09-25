using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour
{
    //public bool soundFXMuted = false;


    public void SaveOnOffPrefs(string settingName, bool settingBool)
    {
        PlayerPrefs.SetInt(settingName, settingBool ? 0 : 1);
    }

    public void LoadOnOffPrefs(string settingName, bool settingBool)
    {
        settingBool = PlayerPrefs.GetInt(settingName) == 1 ? true : false;
    }
}
