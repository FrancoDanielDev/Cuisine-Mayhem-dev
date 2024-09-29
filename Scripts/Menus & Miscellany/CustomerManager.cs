using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;

    private void Start()
    {
        CustomerCounter();
        EventManager.instance.Subscribe(EventManager.NameEvent.CustomerLeaves, CustomerCounter);
        EventManager.instance.Subscribe(EventManager.NameEvent.FinishLevel, SatisfiedCustomersText);
    }

    #region Modules

    private void CustomerCounter(params object[] parameters)
    {
        _uiManager.counterText.text = GameManager.instance.satisfiedCustomers + " / " + LevelManager.instance.requiredCustomers;
    }

    private void SatisfiedCustomersText(params object[] parameters)
    {
        Time.timeScale = 0;
        _uiManager.satisfiedCustomerText.text = GameManager.instance.satisfiedCustomers.ToString();
        _uiManager.finishedLevelScreen.SetActive(true);
        AudioManager.instance.Play("Ending");
        AudioManager.instance.Stop("Level1");
        AudioManager.instance.Stop("Level2");
        AudioManager.instance.Stop("Washing");
        AudioManager.instance.Play("EndScreenMusic");

        if (GameManager.instance.satisfiedCustomers >= LevelManager.instance.requiredCustomers && 
            StaminaSystem.instance != null && StaminaSystem.instance.AmmountOfStamina > 1)
        {
            _uiManager.coins.text = "+ " + 2;
            SaveWithPlayerPrefs.instance.CurrencyUp(2);
            PlayerPrefs.SetInt("reduceStamina", 2);
        }
        else if (StaminaSystem.instance != null && StaminaSystem.instance.AmmountOfStamina > 0)
        {
            _uiManager.coins.text = "+ " + 1;
            SaveWithPlayerPrefs.instance.CurrencyUp(1);
            PlayerPrefs.SetInt("reduceStamina", 1);
        }
        else
        {
            _uiManager.coins.text = "+ " + 0;
        }

        EventManager.instance.Unsubscribe(EventManager.NameEvent.FinishLevel, SatisfiedCustomersText);
    }

    #endregion

}
