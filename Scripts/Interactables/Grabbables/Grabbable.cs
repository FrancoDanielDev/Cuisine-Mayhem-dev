using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Grabbable : MonoBehaviour
{
    public virtual void ChangeObjectPosition(Transform newObjectPos)
    {
        if (gameObject.GetComponent<IPlate>() == null)
        {
            transform.parent = newObjectPos;
            transform.position = newObjectPos.position;
            transform.rotation = newObjectPos.rotation;
        }
        else
        {
            var nm = 0.48f;
            var sc = new Vector3(nm, nm, nm);

            transform.position = newObjectPos.position;
            transform.rotation = newObjectPos.rotation;
            transform.parent = newObjectPos;

            transform.localScale = sc;
        }
    }

    // These Modules would be use in case there was a Grabbable object that does not depend on an Intermediary.

    /*public virtual void CheckPermission(Grabbable obj, Transform newPos)
    {
        if (obj == null)
            TakeItem(newPos);
    }*/

    /*protected virtual void TakeItem(Transform newPos)
    {
        GameManager.instance.currentItemInHand = this;
        //_hitbox.enabled = false;
        ChangeObjectPosition(newPos);
    }*/
}
