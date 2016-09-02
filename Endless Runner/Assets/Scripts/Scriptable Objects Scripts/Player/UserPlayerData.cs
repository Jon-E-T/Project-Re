using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New User Player", menuName = "Characters/Player Character/New Player Character", order = 1)]
public class UserPlayerData : ScriptableObject
{
    public string playerName = "Enter Player Name";
    public float playerMovmentSpeed;
    public float playerJumpForce;
    public float playerJumpTime;
    public AudioClip playerJumpSound;
    public AudioClip playerDeathSound;
}
