using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoCard : MonoBehaviour
{
    [HideInInspector] public string foodName;

    [Header("DRAGGABLE VARIABLES")]
    [SerializeField] private Slider _slider;
    [SerializeField] private CardData[] _cardData;

    private delegate void MyDelegate();
    private MyDelegate _myDelegate = delegate { };

    private ObjectPool<InfoCard> _referenceBack;

    [System.Serializable] 
    public struct CardData
    {
        public FoodManager.FullMeals name;
        public GameObject obj;
    }

    private void Update()
    {
        _myDelegate();
    }

    #region POOL MODULES

    public static void TurnOn(InfoCard c)
    {
        c.gameObject.SetActive(true);
    }

    public static void TurnOff(InfoCard c)
    {
        c.gameObject.SetActive(false);
    }

    public void Create(ObjectPool<InfoCard> op)
    {
        _referenceBack = op;
    }

    #endregion

    public void CreateCard(string food, float maxValueSlider)
    {
        foreach (var item in _cardData)
        {
            if (item.name.ToString() == food) item.obj.SetActive(true);
            else                              item.obj.SetActive(false);
        }

        _slider.value = 0;
        _slider.maxValue = maxValueSlider;
        foodName = food;
        _myDelegate = SliderBehaviour;
    }

    public void ReturnObject()
    {
        _referenceBack.ReturnObject(this);
    }

    private void SliderBehaviour()
    {
        _slider.value += Time.deltaTime;

        if (_slider.value >= _slider.maxValue)
        {
            OrderManager.instance.LeaveUnsatisfied(this);
            AudioManager.instance.Play("ClientGone");
            _myDelegate = delegate { };
        }
    }
}
