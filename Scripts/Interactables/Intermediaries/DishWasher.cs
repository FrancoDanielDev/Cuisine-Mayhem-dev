using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishWasher : Intermediary, IPlayerRequired
{
    [SerializeField] private float _washCooldown;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private Transform _playerWashingLocation;
    [SerializeField] private bool _tutorial = false;

    private Player _player;

    #region Intermediary Modules

    public override void CheckPermission(Grabbable obj, Transform newPos)
    {
        if (obj != null && item == null)
            SetObject(obj, _itemLocation);
    }

    protected override void SetObject(Grabbable obj, Transform newPos)
    {
        if (obj.GetComponent<IPlate>() != null && obj.GetComponent<IPlate>().IsEmptyAndDirty())
        {
            base.SetObject(obj, newPos);
            StartCoroutine(WashPlate(obj));
        }
    }

    #endregion

    #region DishWasher Modules

    public void GrantPlayer(Player player)
    {
        _player = player;
    }

    private IEnumerator WashPlate(Grabbable plate)
    {
        _player.StopMovement();
        _player.transform.position = _playerWashingLocation.position;
        _player.transform.rotation = _playerWashingLocation.rotation;
        _player.view.Washing(true);
        AudioManager.instance.Play("Washing");
        _particle.Play();

        yield return new WaitForSeconds(_washCooldown);

        TakeObject(plate, _player.PlayerHand);
        _player.ActivateMovement();
        plate.GetComponent<IPlate>().SelectCleanPlate();
        _player.view.Washing(false);
        AudioManager.instance.Stop("Washing");
        _particle.Stop();

        if (_tutorial)
        {
            _tutorial = false;
            TutorialManager.instance.TriggerFunction();
        }
    }

    #endregion

}
