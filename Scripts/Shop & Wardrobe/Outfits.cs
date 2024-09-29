using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outfits : MonoBehaviour
{
    public ScriptableItem[] item = new ScriptableItem[0];

    private void Start()
    {
        SaveWithPlayerPrefs.instance.amountOfShopItems = item.Length;
    }
}
