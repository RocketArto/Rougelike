using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ImageSourceListScriptableObject", order = 2)]
public class ImageSourceListScriptableObject : ScriptableObject
{
    public Sprite[] spriteList;
}
