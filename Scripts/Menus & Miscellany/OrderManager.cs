using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;

    [Header("DRAGGABLE VARIABLES")]
    [SerializeField] private InfoCard _prefab;
    [SerializeField] private RectTransform _initialWaypoint;
    [SerializeField] private RectTransform[] _wayPoint;

    private float _myTime;
    private ObjectPool<InfoCard> _pool;
    private InfoCard[] _infoCard;

    private delegate void MyDelegate();
    private MyDelegate _myDelegate = delegate { };

    private void Awake()
    {
        if (instance == null) instance = this;

        _pool = new ObjectPool<InfoCard>(Factory, InfoCard.TurnOn, InfoCard.TurnOff, _wayPoint.Length);
    }

    private InfoCard Factory()
    {
        return Instantiate(_prefab, _initialWaypoint);
    }

    private void Start()
    {
        _infoCard = new InfoCard[_wayPoint.Length];
        CreateInitialCustomers();
        _myDelegate = MyTimer;
    }

    private void Update()
    {
        _myDelegate();
    }

    private void CreateInitialCustomers()
    {
        if (LevelManager.instance.initialRandomOrders >= 1)
        {
            for (int i = 0; i < LevelManager.instance.initialRandomOrders && i < _wayPoint.Length; i++)
            {
                Customer(LevelManager.instance.MenuRandomizer(), i);
            }
        }
        else
        {
            int index = 0;

            foreach (var item in LevelManager.instance.initialSpecificOrders)
            {
                if (index >= _wayPoint.Length) return;

                Customer(item.name.ToString(), index);
                index++;
            }
        }
    }

    private void NewCustomer()
    {
        int index = 0;

        for (int i = 0; i < _infoCard.Length; i++)
        {
            if (_infoCard[i] == null)
            {
                index = i;
                break;
            }

            else if (i == _infoCard.Length - 1 && _infoCard[i] != null)
            {
                _myDelegate = delegate { };
                _myTime = 0;
                return;
            }
        }

        Customer(LevelManager.instance.MenuRandomizer(), index);
    }

    private void Customer(string menu, int index)
    {
        var newCustomer = _pool.GetObject();
        newCustomer.Create(_pool);
        newCustomer.CreateCard(menu, LevelManager.instance.customerPatience);
        newCustomer.transform.position = _wayPoint[index].position;
        newCustomer.transform.SetParent(_wayPoint[index]);
        _infoCard[index] = newCustomer;
    }

    private void MyTimer()
    {
        _myTime += Time.deltaTime;

        if (_myTime >= LevelManager.instance.newCustomerCooldown)
        {
            NewCustomer();
            _myTime = 0;
        }
    }

    public void FoodDelivered(string food)
    {
        if (_infoCard[0] == null)
        {
            Debug.LogError("Something went wrong with the Info Cards.");
            return;
        }

        for (int i = 0; i < _infoCard.Length; i++)
        {
            if (_infoCard[i] != null && _infoCard[i].foodName == food)
            {
                LeaveSatisfied(_infoCard[i]);
                _infoCard[i] = null;
                return;
            }
        }

        LeaveUnsatisfied(_infoCard[0]);
    }

    private void LeaveSatisfied(InfoCard card)
    {
        GameManager.instance.satisfiedCustomers++;
        AudioManager.instance.Play("Correct");
        card.ReturnObject();
        EventManager.instance.Trigger(EventManager.NameEvent.CustomerLeaves);

        StartCoroutine(CustomerLeaves());
    }

    public void LeaveUnsatisfied(InfoCard card)
    {
        for (int i = 0; i < _infoCard.Length; i++)
        {
            if (_infoCard[i] == card)
            {
                _infoCard[i].ReturnObject();
                _infoCard[i] = null;
                break;
            }
        }

        AudioManager.instance.Play("Wrong");
        EventManager.instance.Trigger(EventManager.NameEvent.CustomerLeaves);

        StartCoroutine(CustomerLeaves());
    }

    private IEnumerator CustomerLeaves()
    {
        yield return new WaitForSeconds(0.00001f);

        for (int i = 0; i < _infoCard.Length; i++)
        {
            if (_infoCard[i] == null || i == 0) continue;

            if (_infoCard[i - 1] == null)
            {
                _infoCard[i].transform.position = _wayPoint[i - 1].position;
                _infoCard[i].transform.SetParent(_wayPoint[i - 1]);
                _infoCard[i - 1] = _infoCard[i];
                _infoCard[i] = null;
            }
        }

        NewCustomer();
    }
}
