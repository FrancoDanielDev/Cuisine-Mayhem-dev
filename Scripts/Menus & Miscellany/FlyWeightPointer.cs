using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightPointer : MonoBehaviour
{
    public static readonly Flyweight PlayerBase = new Flyweight
    {
        speed = 4f
    };

    public static readonly Flyweight PlayerDash = new Flyweight
    {
        speed = 1,
        time = 0.25f,
        cooldown = 1.5f
    };
}
