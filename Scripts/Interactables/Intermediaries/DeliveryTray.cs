using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryTray : Intermediary
{
    [SerializeField] private float _yPlateStackAxis;
    [SerializeField] private bool _tutorial = false;

    private List<Grabbable> _plate = new List<Grabbable>();

    public override void CheckPermission(Grabbable obj, Transform newPos)
    {
        if (obj == null && item != null && _plate.Count != 0)
            TakeObject(obj, newPos);

        else if (obj != null)
            SetObject(obj, _itemLocation);
    }

    protected override void TakeObject(Grabbable obj, Transform newPos)
    {
        GameManager.instance.currentItemInHand = item;
        item.ChangeObjectPosition(newPos);

        for (int i = _plate.Count - 1; i >= 0; i--)
        {
            if (_plate[i] == item)
            {
                if (i - 1 >= 0) item = _plate[i - 1];
                else            item = null;

                _plate.RemoveAt(i);

                return;
            }
        }

        AudioManager.instance.Play("Interactions");
    }

    protected override void SetObject(Grabbable obj, Transform newPos)
    {
        IPlate plate = obj.GetComponent<IPlate>();

        if (plate == null || !plate.ContainsFood()) return;

        GameManager.instance.currentItemInHand = null;
        obj.ChangeObjectPosition(_itemLocation);
        plate.SetPlateToDirtyAndEmpty();
        OrderManager.instance.FoodDelivered(plate.FoodName());
        _plate.Add(obj);
        item = obj;

        if (_tutorial)
        {
            _tutorial = false;
            TutorialManager.instance.TriggerFunction();
        }

        if (_plate.Count == 1)
        {
            _plate[0].ChangeObjectPosition(_itemLocation);
            return;
        }

        _plate[_plate.Count - 1].ChangeObjectPosition(_plate[_plate.Count - 2].transform);
        _plate[_plate.Count - 1].transform.position += new Vector3(0, _yPlateStackAxis, 0);
    }

}
