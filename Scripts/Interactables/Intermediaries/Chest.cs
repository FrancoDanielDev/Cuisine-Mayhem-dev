using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Intermediary
{
    [SerializeField] private FoodManager.Ingredients _selectFood;
    [SerializeField] private Transform _imageLocation;
    [SerializeField] private int _foodsToGenerate = 5;
    [SerializeField] private bool _tutorial = false;

    private GrabbableFood _food;
    private ObjectPool<GrabbableFood> _pool;

    private void Start()
    {
        foreach (var item in FoodManager.instance.myIngredients)
        {
            if (item.name == _selectFood)
            {
                Instantiate(item.cardImage, _imageLocation);
                _food = item.foodPrefab;
                break;
            }
        }

        _pool = new ObjectPool<GrabbableFood>(Factory, GrabbableFood.TurnOn, GrabbableFood.TurnOff, _foodsToGenerate);
    }

    #region Modules

    private GrabbableFood Factory()
    {
        return Instantiate(_food, transform);
    }

    private GrabbableFood GiveFood(Transform transf)
    {
        var newFood = _pool.GetObject();

        newFood.Create(_pool);
        newFood.ChangeObjectPosition(transf);
        GameManager.instance.currentItemInHand = newFood;

        if (_tutorial) TutorialManager.instance.TriggerFunction();

        return newFood;
    }

    public override void CheckPermission(Grabbable obj, Transform newPos)
    {
        if (obj == null)
        {
            GiveFood(newPos);
            AudioManager.instance.Play("Interactions");
        }
        else
        {
            AudioManager.instance.Play("Nope");
        }
    }

    #endregion

}
