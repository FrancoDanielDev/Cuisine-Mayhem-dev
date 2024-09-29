using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlate
{
    string FoodName();

    void SelectCleanPlate();

    void EmptyPlate();

    void SetPlateToDirtyAndEmpty();

    void SetFoodInPlate(GrabbableFood item, string foodName);

    bool IsEmptyAndClean();

    bool IsEmptyAndDirty();

    bool ContainsFood();
}
