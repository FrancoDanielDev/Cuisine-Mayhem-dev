using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static FoodManager instance;

    public MyIngredients[] myIngredients;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public enum FullMeals
    {
        Steak,
        Soup
    }

    public enum Ingredients
    {
        RawSteak,
        Carrot,
        Onion
    }

    [System.Serializable]
    public struct MyIngredients
    {
        public Ingredients name;
        public GameObject cardImage;
        public GrabbableFood foodPrefab;
    }

}
