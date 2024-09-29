using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrabbableFood : Grabbable
{  
    private ObjectPool<GrabbableFood> _referenceBack;


    public void ReturnObjectToPool()
    {
        _referenceBack.ReturnObject(this);
        RestartObject();
    }

    protected virtual void RestartObject() { }

    #region POOL MODULES

    public static void TurnOn(GrabbableFood c)
    {
        c.gameObject.SetActive(true);
    }

    public static void TurnOff(GrabbableFood c)
    {
        c.gameObject.SetActive(false);
    }

    public void Create(ObjectPool<GrabbableFood> op)
    {
        _referenceBack = op;
    }

    #endregion

}
