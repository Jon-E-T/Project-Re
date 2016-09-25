using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Power-Up Settings", menuName = "Collectible Items/Power-Ups/New Power-Up", order = 1)]
public class PowerupItemData : ScriptableObject
{
    public string powerupName = "Input Power-Up Name";
    public Sprite powerupSprite;
    public Color powerupColor = Color.white;
    public bool playerFlight = false;
    public bool noSpikes = false;
    public float powerUpActiveTime;

}
