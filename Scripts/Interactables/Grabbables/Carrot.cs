using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : GrabbableFood, ICarrot, IVegetable
{
    [SerializeField] private GameObject _carrot;
    [SerializeField] private GameObject _soup;

    public void BoiledSoup()
    {
        _carrot.SetActive(false);
        _soup.SetActive(true);
    }

    protected override void RestartObject()
    {
        _carrot.SetActive(true);
        _soup.SetActive(false);
    }
}
