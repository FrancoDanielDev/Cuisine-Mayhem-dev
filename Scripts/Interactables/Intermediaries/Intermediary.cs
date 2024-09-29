using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Intermediary : MonoBehaviour
{
    [Tooltip ("If there is a Grabbable object already placed on, you must assigned this variable. It cannot be null.")]
    public Grabbable item;

    [Tooltip ("Here will be set an Ingredient or a Plate.")]
    [SerializeField] protected Transform _itemLocation;

    public Transform ObjectPosition
    {
        get { return _itemLocation; }
    }

    public virtual void CheckPermission(Grabbable obj, Transform newPos)
    {
        if (obj == null && item != null)
            TakeObject(obj, newPos);

        else if (obj != null && item == null)
            SetObject(obj, _itemLocation);

        else
            AudioManager.instance.Play("Nope");
    }

    protected virtual void TakeObject(Grabbable obj, Transform newPos)
    {
        GameManager.instance.currentItemInHand = item;
        item.ChangeObjectPosition(newPos);
        item = null;
        AudioManager.instance.Play("Interactions");
    }

    protected virtual void SetObject(Grabbable obj, Transform newPos)
    {
        GameManager.instance.currentItemInHand = null;
        obj.ChangeObjectPosition(newPos);
        item = obj;
        AudioManager.instance.Play("Interactions");
    }
}
