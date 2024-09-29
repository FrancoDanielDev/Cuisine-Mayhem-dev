using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hats : MonoBehaviour
{
    [SerializeField] private GameObject[] _hat = new GameObject[0];

    private void Start()
    {
        for (int i = 0; i < _hat.Length; i++)
        {
            if (PlayerPrefs.HasKey("hat" + i) && PlayerPrefs.GetInt("hat" + i) == 1)
            {
                _hat[i].SetActive(true);
            }
        }      
    }
}
