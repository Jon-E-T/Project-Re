using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Platform Config", menuName = "Platforms/Platform Config", order = 1)]
public class PlatformConfigData : ScriptableObject
{
    public string platformName;
    public Sprite platformSprite;
    public Color platformColor;
    public PhysicsMaterial2D physicMaterial2D;
}
