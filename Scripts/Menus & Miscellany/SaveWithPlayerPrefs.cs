using UnityEngine;
using UnityEngine.UI;

public class SaveWithPlayerPrefs : MonoBehaviour
{
    public static SaveWithPlayerPrefs instance;

    [HideInInspector] public int amountOfShopItems;
    [HideInInspector] public int hasSeenTutorial;
    [HideInInspector] public int level;
    public int currency;

    [SerializeField] private Text _currencyText = null;
    [SerializeField] private GameObject _continueText = null;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        LoadGame();

        if (_currencyText != null)
            _currencyText.text = currency.ToString();

        if (_continueText != null && hasSeenTutorial == 0)
            _continueText.SetActive(false);
    }

    public void CurrencyUp(int sum)
    {
        currency += sum;
        SaveGame();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Currency", currency);
        PlayerPrefs.SetInt("HasSeenTutorial", hasSeenTutorial);    

        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("Currency"))
            currency = PlayerPrefs.GetInt("Currency");

        if (PlayerPrefs.HasKey("HasSeenTutorial"))
            hasSeenTutorial = PlayerPrefs.GetInt("HasSeenTutorial");
    }

    public void ResetData()
    {
        if (PlayerPrefs.HasKey("Currency"))        
            PlayerPrefs.SetInt("Currency", 0);

        if (PlayerPrefs.HasKey("HasSeenTutorial")) 
            PlayerPrefs.SetInt("HasSeenTutorial", 0);

        for (int i = 0; i < amountOfShopItems; i++)
        {
            PlayerPrefs.SetInt("shopItem" + i, 0);
            PlayerPrefs.SetInt("wardrobeItem" + i, 0);
            PlayerPrefs.SetInt("hat" + i, 0);
        }

        PlayerPrefs.SetInt("activateStamina", 0);
        PlayerPrefs.SetInt("reduceStamina", 0);

        PlayerPrefs.Save();
    }  
}
