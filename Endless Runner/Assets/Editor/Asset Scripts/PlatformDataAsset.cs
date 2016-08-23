using UnityEngine;
using UnityEditor;

public class PlatformDataAsset
{
    [MenuItem("Assets/Create/Platform Data")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<PlatformData>();
    }
}