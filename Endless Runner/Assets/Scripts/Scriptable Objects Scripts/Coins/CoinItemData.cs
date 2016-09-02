using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Coin Item Settings", menuName = "Collectible Items/Coins/New Coin", order = 1)]
public class CoinItemData : ScriptableObject
{
    public string coinName = "Enter Coin Name";
    public Sprite coinSprite;
    public Color coinColor = Color.white;
    public int coinValue;
    public AudioClip coinSound;

}
