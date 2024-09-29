using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    PlayerModel _model;
    PlayerView _view;

    public PlayerController(PlayerModel m, PlayerView v)
    {
        _model = m;
        _view = v;
    }

    public void ArtificialUpdate()
    {
        if (JoyController.instance.GetMovementInput().magnitude > 0)
        {
            _model.Movement(JoyController.instance.GetMovementInput());
            _view.Walking(true);
        }
        else
        {
            _view.Walking(false);
        }

        if (GameManager.instance.currentItemInHand != null)
            _view.WithPlate(true);
        else
            _view.WithPlate(false);

    }
}
