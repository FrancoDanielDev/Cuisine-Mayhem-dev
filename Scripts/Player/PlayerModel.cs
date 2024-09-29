using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    Transform _transform;
    Rigidbody _rb;
    float _speed;


    public PlayerModel(Transform transform, Rigidbody rb, float speed)
    {
        _transform = transform;
        _rb = rb;
        _speed = speed;
    }

    public void Movement(Vector3 movement)
    {
        _rb.position += Time.deltaTime * _speed * movement * Time.timeScale;
        _transform.rotation = Quaternion.LookRotation(movement);
    }

    public void ExecuteInteraction(GameObject interactableObj, Player player, Transform playerHand)
    {
        var intermediary = interactableObj.GetComponent<Intermediary>();
        var requirement = interactableObj.GetComponent<IPlayerRequired>();

        if (intermediary)
        {
            if (requirement != null)
                requirement.GrantPlayer(player);

            intermediary.CheckPermission(GameManager.instance.currentItemInHand, playerHand);
        }
    }
    

}
