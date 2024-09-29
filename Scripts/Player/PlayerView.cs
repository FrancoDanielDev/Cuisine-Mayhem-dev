using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView
{
    Animator _animator;

    public PlayerView(Animator am)
    {
        _animator = am;
    }

    public void Walking(bool value)
    {
        _animator.SetBool("Walking", value);
    }

    public void Washing(bool value)
    {
        _animator.SetBool("Washing", value);
    }

    public void WithPlate(bool value)
    {
        _animator.SetBool("WithPlate", value);
    }

}
