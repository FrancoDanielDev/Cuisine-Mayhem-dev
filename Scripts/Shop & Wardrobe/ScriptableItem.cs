using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "CustomScriptables/Item")]
public class ScriptableItem : ScriptableObject
{
    public string itemName;
    public int cost;
    public Sprite image;
}
