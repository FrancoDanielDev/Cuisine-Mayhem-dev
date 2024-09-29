using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Intermediary
{
    [SerializeField] private float _cookCooldown;
    [SerializeField] private AudioSource _meatAudio;
    [SerializeField] private AudioSource _boilAudio;
    [SerializeField] private GameObject _particle;
    [SerializeField] private UtensilSelection _utensilSelection;
    [SerializeField] private OvenData[] _ovenData;
    [SerializeField] private bool _tutorial = false;

    private string _foodName;
    private bool _available = false;
    private bool _carrotSet = false;
    private bool _onionSet = false;

    private GrabbableFood _reservedIngredient;

    public enum UtensilSelection
    {
        FryingPan,
        SaucePan
    }

    [System.Serializable]
    public struct OvenData
    {
        public UtensilSelection name;
        public FoodManager.FullMeals mealToCook;
        public GameObject obj;
    }

    private void Start()
    {
        SelectUtensilAndMealToCook();
        EventManager.instance.Subscribe(EventManager.NameEvent.FinishLevel, StopNoise);
    }

    #region Intermediary Modules

    private void StopNoise(params object[] parameters)
    {
        if (_meatAudio.isPlaying)
            _meatAudio.Stop();

        if (_boilAudio.isPlaying)
            _boilAudio.Stop();
    }

    public override void CheckPermission(Grabbable obj, Transform newPos)
    {
        if (obj == null) return;

        if (item != null && _available)
            TakeObject(obj, newPos);

        else if (item == null)
            SetObject(obj, _itemLocation);
    }

    protected override void TakeObject(Grabbable obj, Transform newPos)
    {
        var plate = obj.GetComponent<IPlate>();
        var food = item.GetComponent<GrabbableFood>();

        _available = false;

        // With a Plate in hand

        if (plate != null && plate.IsEmptyAndClean())
        {
            plate.SetFoodInPlate(food, _foodName);
            item = null;

            if (_tutorial)
            {
                _tutorial = false;
                TutorialManager.instance.TriggerFunction();
            }

            AudioManager.instance.Play("Interactions");
        }
    }

    protected override void SetObject(Grabbable obj, Transform newPos)
    {
        var meat = obj.GetComponent<IMeat>();
        var properMeal = obj.GetComponent<IProperMeal>();
        var veg = obj.GetComponent<IVegetable>();

        // Frying Pan

        if (_utensilSelection == UtensilSelection.FryingPan)
        {
            if (meat != null && properMeal != null && !properMeal.IsMealProper())
            {
                base.SetObject(obj, newPos);
                StartCoroutine(CookMeat(obj.GetComponent<IMeat>()));
            }
        }

        // Sauce Pan

        else if (_utensilSelection == UtensilSelection.SaucePan)
        {
            if (veg != null)
            {
                if ((_carrotSet && obj.GetComponent<ICarrot>() != null) || (_onionSet && obj.GetComponent<IOnion>() != null)) return;

                base.SetObject(obj, newPos);
                SetIngredient(obj);
            }
        }
    }

    #endregion

    #region Oven Modules

    private void SelectUtensilAndMealToCook()
    {
        foreach (var item in _ovenData)
        {
            if (item.name == _utensilSelection)
            {
                item.obj.SetActive(true);
                _foodName = item.mealToCook.ToString();
            }
        }
    }

    private IEnumerator CookMeat(IMeat meat)
    {
        _available = false;

        _meatAudio.Play();
        _particle.SetActive(true);

        yield return new WaitForSeconds(_cookCooldown);

        if (_tutorial) TutorialManager.instance.TriggerFunction();

        _meatAudio.Stop();
        _particle.SetActive(false);

        _available = true;
        meat.CookedMeat();
    }

    private void SetIngredient(Grabbable veg)
    {
        // Is there a Carrot?

        if (!_carrotSet && veg.GetComponent<ICarrot>() != null)
        {
            _carrotSet = true;
        }

        // Is there an Onion?

        else if (!_onionSet && veg.GetComponent<IOnion>() != null)
        {
            _onionSet = true;
        }

        // Result

        if (_carrotSet && _onionSet)
        {
            StartCoroutine(BoilSoup(veg.GetComponent<IVegetable>()));
        }
        else
        {
            _reservedIngredient = veg.GetComponent<GrabbableFood>();
            item = null;
        }
    }

    private IEnumerator BoilSoup(IVegetable veg)
    {
        _available = false;

        _boilAudio.Play();
        _particle.SetActive(true);

        yield return new WaitForSeconds(_cookCooldown);

        _carrotSet = false;
        _onionSet = false;

        _boilAudio.Stop();
        _particle.SetActive(false);

        _available = true;
        _reservedIngredient.ReturnObjectToPool();
        _reservedIngredient = null;
        veg.BoiledSoup();
    }

    #endregion

}
