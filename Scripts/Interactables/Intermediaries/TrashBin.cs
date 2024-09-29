using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : Intermediary
{
    public override void CheckPermission(Grabbable obj, Transform newPos)
    {
        if (obj != null)
            SetObject(obj, _itemLocation);
    }

    protected override void SetObject(Grabbable obj, Transform newPos)
    {
        var plate = obj.GetComponent<IPlate>();
        var food = obj.GetComponent<GrabbableFood>();

        // Plates

        if (plate != null && plate.ContainsFood())
        {
            obj.GetComponent<IPlate>().EmptyPlate();
            AudioManager.instance.Play("Interactions");
        }

        // Grabbable Foods

        else if (food)
        {
            food.ReturnObjectToPool();
            GameManager.instance.currentItemInHand = null;
            AudioManager.instance.Play("Interactions");
        }
    }
}
