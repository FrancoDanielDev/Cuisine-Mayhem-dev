using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onion : GrabbableFood, IOnion, IVegetable
{
    [SerializeField] private GameObject _onion;
    [SerializeField] private GameObject _soup;

    public void BoiledSoup()
    {
        _onion.SetActive(false);
        _soup.SetActive(true);
    }

    protected override void RestartObject()
    {
        _onion.SetActive(true);
        _soup.SetActive(false);
    }
}
