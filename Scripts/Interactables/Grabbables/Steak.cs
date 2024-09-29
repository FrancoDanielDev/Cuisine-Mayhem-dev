using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steak : GrabbableFood, IMeat, IProperMeal
{
    [SerializeField] private GameObject _rawSteak;
    [SerializeField] private GameObject _cookedSteak;

    public void CookedMeat()
    {
        _rawSteak.SetActive(false);
        _cookedSteak.SetActive(true);
    }

    public bool IsMealProper()
    {
        return _cookedSteak.activeSelf;
    }

    protected override void RestartObject()
    {
        _rawSteak.SetActive(true);
        _cookedSteak.SetActive(false);
    }
}
