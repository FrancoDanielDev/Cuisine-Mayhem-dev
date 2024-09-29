using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("TIMER VARIABLES")]
    public bool timerOn;
    [Tooltip ("In seconds.")]
    public float levelDuration;

    [Header("ORDER VARIABLES")]
    [Tooltip("Leave the value in Zero if you are not interested in Random menues.")]
    public int initialRandomOrders = 2;
    [Space]
    [Tooltip("Do not assign values if you are not interested in Specific menues.")]
    public InitialSpecificOrders[] initialSpecificOrders;
    [Space]
    [SerializeField] private LevelFoodsPool[] levelFoodsPool;

    [Header("CUSTOMER VARIABLES")]
    public float newCustomerCooldown = 50;
    public float customerPatience = 45;
    public int requiredCustomers;

    [System.Serializable]
    public struct LevelFoodsPool
    {
        public FoodManager.FullMeals name;
    }

    [System.Serializable]
    public struct InitialSpecificOrders
    {
        public FoodManager.FullMeals name;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public string MenuRandomizer()
    {
        List<string> myList = new List<string>();

        foreach (var item in levelFoodsPool)
        {
            myList.Add(item.name.ToString());
        }

        int luckyNumber = Random.Range(0, myList.Count);

        return myList[luckyNumber];
    }

}
