using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenCabinet : Intermediary
{
    [SerializeField] private bool _tutorial = false;

    protected override void TakeObject(Grabbable obj, Transform newPos)
    {
        base.TakeObject(obj, newPos);

        if (_tutorial)
        {
            _tutorial = false;
            TutorialManager.instance.TriggerFunction();
        }
    }

    /*public override void CheckPermission(Grabbable obj, Transform newPos)
    {
        if (item != null)
            TakeObject(obj, newPos);

        else if (obj != null)
            SetObject(obj, _itemLocation);
    }

    protected override void TakeObject(Grabbable obj, Transform newPos)
    {
        if (obj != null)
        {
            if (obj.GetComponent<IPlate>() != null && obj.GetComponent<IPlate>().IsEmptyAndClean()
                   && item.GetComponent<IProperMeal>() != null && item.GetComponent<IProperMeal>().IsMealProper())
            {
                if (item.GetComponent<IFoodEnumRequired>() == null)
                {
                    Debug.LogWarning("ERROR. Food does not contain an ID from FoodManager.");
                    return;
                }

                obj.GetComponent<IPlate>().SetFoodInPlate(item.GetComponent<IFoodEnumRequired>().FoodSelection());
                item.DestroyItem();
                item = null;
            }
        }
        else
        {
            base.TakeObject(obj, newPos);
        }
    }


    protected override void SetObject(Grabbable obj, Transform newPos)
    {
        if (item != null)
        {
            Debug.Log(item.GetComponent<IPlate>().IsEmptyAndClean());
            Debug.Log(obj.GetComponent<IProperMeal>().IsMealProper());
            if (item.GetComponent<IPlate>() != null && item.GetComponent<IPlate>().IsEmptyAndClean()
                   && obj.GetComponent<IProperMeal>() != null && obj.GetComponent<IProperMeal>().IsMealProper())
            {
                if (obj.GetComponent<IFoodEnumRequired>() == null)
                {
                    Debug.LogWarning("ERROR. Food does not contain an ID from FoodManager.");
                    return;
                }
                Debug.Log("FUNCA");
                item.GetComponent<IPlate>().SetFoodInPlate(obj.GetComponent<IFoodEnumRequired>().FoodSelection());
                obj.DestroyItem();
            }
        }
        else
        {
            base.SetObject(obj, newPos);
        }
        
    }*/
}
