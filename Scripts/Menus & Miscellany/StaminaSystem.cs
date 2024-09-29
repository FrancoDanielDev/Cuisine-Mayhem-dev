using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class StaminaSystem : MonoBehaviour
{
    public static StaminaSystem instance;

    [SerializeField] private bool _mainMenu = false;

    public int AmmountOfStamina { get => _staminaAmmount; }

    [Header("COMMON VARIABLES")]
    [SerializeField] private int _maxStamina;
    [SerializeField] private float _timeToRecharge;

    [Header("DRAGGABLE VARIABLES")]
    [SerializeField] private TextMeshProUGUI _staminaText = null;
    [SerializeField] private TextMeshProUGUI _timerText = null;
    [SerializeField] private TextMeshProUGUI _fullTimeText = null;

    private int _staminaAmmount;
    private bool _restoring;

    private DateTime _nextStaminaTime;
    private DateTime _lastStaminaTime;

    private void Awake()
    {
        if (instance == null) instance = this;
        else                  Destroy(gameObject);
    }

    private void Start()
    {
        if (_mainMenu)
            Initially();
        else
            _staminaAmmount = PlayerPrefs.GetInt("currentStamina");
    }

    #region Modules

    private void Initially()
    {
        if (!PlayerPrefs.HasKey("activateStamina") || PlayerPrefs.GetInt("activateStamina") == 0)
            PlayerPrefs.SetInt("currentStamina", _maxStamina);

        MaxCoinsText();
        PlayerPrefs.SetInt("activateStamina", 1);
        LoadTime();
        StartCoroutine(RestoreEnergy());

        if (PlayerPrefs.HasKey("reduceStamina") && PlayerPrefs.GetInt("reduceStamina") > 0)
        {
            UseEnergy(PlayerPrefs.GetInt("reduceStamina"));
            PlayerPrefs.SetInt("reduceStamina", 0);
        }
    }

    public void UseEnergy(int energyAmmount)
    {
        if (_staminaAmmount - energyAmmount >= 0)
        {
            _staminaAmmount -= energyAmmount;
            UpdateStamina();

            if (!_restoring)
            {
                _nextStaminaTime = AddDuration(DateTime.Now, _timeToRecharge);
                StartCoroutine(RestoreEnergy());
            }
        }
        else
        {
            Debug.Log("Max. Capacity");
        }
    }

    public void FullEnergy()
    {
        _staminaAmmount = _maxStamina;
        UpdateStamina();
        MaxCoinsText();
    }

    private IEnumerator RestoreEnergy()
    {
        UpdateStamina();
        _restoring = true;

        while (_staminaAmmount < _maxStamina)
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime nextDateTime = _nextStaminaTime;
            bool staminaAdd = false;

            while (currentDateTime > nextDateTime)
            {
                if (_staminaAmmount < _maxStamina)
                {
                    _staminaAmmount += 1;
                    staminaAdd = true;
                    UpdateStamina();
                    DateTime timeToAdd = DateTime.Now;

                    if (_lastStaminaTime > nextDateTime)
                        timeToAdd = _lastStaminaTime;
                    else
                        timeToAdd = nextDateTime;

                    nextDateTime = AddDuration(timeToAdd, _timeToRecharge);
                }
                else
                {
                    break;
                }
            }

            if (staminaAdd)
            {
                _lastStaminaTime = DateTime.Now;
                _nextStaminaTime = nextDateTime;
            }

            UpdateTimer();
            UpdateStamina();
            SaveTime();

            yield return new WaitForEndOfFrame();
        }

        _restoring = false;
    }

    private DateTime AddDuration(DateTime date, float duration)
    {
        return date.AddSeconds(duration);
    }

    private void UpdateStamina()
    {
        _staminaText.text = _staminaAmmount.ToString() + " / " + _maxStamina.ToString();
    }

    private void MaxCoinsText()
    {
        _timerText.text = "";
        _fullTimeText.text = "...";
    }

    private void UpdateTimer()
    {
        if (_staminaAmmount >= _maxStamina)
        {
            MaxCoinsText();
            return;
        }

        _fullTimeText.text = "New Coin in:";

        TimeSpan timer = _nextStaminaTime - DateTime.Now;
        _timerText.text = timer.Minutes.ToString() + ":" + timer.Seconds.ToString();

        /*var minutes = nextStaminaTime.Minute - DateTime.Now.Minute;
        var seconds = nextStaminaTime.Second - DateTime.Now.Second;
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);*/
    }

    private void LoadTime()
    {
        _staminaAmmount = PlayerPrefs.GetInt("currentStamina");
        _nextStaminaTime = StringToDateTime(PlayerPrefs.GetString("nextStaminaTime"));
        _lastStaminaTime = StringToDateTime(PlayerPrefs.GetString("lastStaminaTime"));
    }

    private void SaveTime()
    {
        PlayerPrefs.SetInt("currentStamina", _staminaAmmount);
        PlayerPrefs.SetString("nextStaminaTime", _nextStaminaTime.ToString());
        PlayerPrefs.SetString("lastStaminaTime", _lastStaminaTime.ToString());
    }

    private DateTime StringToDateTime(string timeString)
    {
        if (string.IsNullOrEmpty(timeString))
            return DateTime.Now;
        else
            return DateTime.Parse(timeString);
    }

    #endregion

}
