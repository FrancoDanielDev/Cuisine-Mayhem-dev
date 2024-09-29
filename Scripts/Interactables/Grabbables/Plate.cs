using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : Grabbable, IPlate
{
    [HideInInspector] public GrabbableFood foodItem;

    [Header("PLATE VARIABLES")]
    [SerializeField] private bool _startsClean = true;

    [Header("DRAGGABLE VARIABLES")]
    [SerializeField] private GameObject[] _cleanAndDirtyPlate;
    [Space]
    [SerializeField] private Transform foodLocation;

    private string _foodName;

    private void Start()
    {
        if (_startsClean) 
            SelectCleanPlate();
        else        
            SelectDirtyPlate();
    }

    #region Modules

    public string FoodName()
    {
        return _foodName;
    }

    public void SetPlateToDirtyAndEmpty()
    {
        SelectDirtyPlate();
        EmptyPlate();
    }

    public bool IsEmptyAndClean()
    {
        if (_cleanAndDirtyPlate[0].activeSelf && foodItem == null) return true;

        return false;
    }

    public bool IsEmptyAndDirty()
    {
        if (_cleanAndDirtyPlate[1].activeSelf && foodItem == null) return true;

        return false;
    }

    public bool ContainsFood()
    {
        if (foodItem) return true;

        return false;
    }

    public void SelectCleanPlate()
    {
        _cleanAndDirtyPlate[0].SetActive(true);
        _cleanAndDirtyPlate[1].SetActive(false);
    }

    public void EmptyPlate()
    {
        foodItem.ReturnObjectToPool();
        foodItem = null;
    }

    public void SetFoodInPlate(GrabbableFood item, string foodName)
    {
        foodItem = item;
        foodItem.ChangeObjectPosition(foodLocation);
        _foodName = foodName;
    }

    private void SelectDirtyPlate()
    {
        _cleanAndDirtyPlate[0].SetActive(false);
        _cleanAndDirtyPlate[1].SetActive(true);
    }

    #endregion

}
