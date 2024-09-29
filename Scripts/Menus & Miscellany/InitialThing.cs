using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialThing : MonoBehaviour
{
    private void Start()
    {
        TheTime();
    }

    public void TheTime()
    {
        Time.timeScale = 1f;
    }
}
