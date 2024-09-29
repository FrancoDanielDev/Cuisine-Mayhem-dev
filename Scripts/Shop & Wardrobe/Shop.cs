using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    [SerializeField] private TMP_Text _currencyText;
    [SerializeField] private ShopItem _shopItemPrefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private Outfits _outfits;
    [SerializeField] private GameObject _areYouSure;
    [SerializeField] private GameObject _shopMenu;

    private ShopItem _item;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        Initially();
    }

    private void Initially()
    {
        AlterCurrencyText();

        for (int i = 0; i < _outfits.item.Length; i++)
        {
            var newButton = Instantiate(_shopItemPrefab, _parent);
            newButton.scriptableItem = _outfits.item[i];
            newButton.index = i;
        }
    }

    public void AlterCurrencyText()
    {
        if (SaveWithPlayerPrefs.instance != null)
            _currencyText.text = SaveWithPlayerPrefs.instance.currency.ToString();
    }

    public void SaveAPurchasableItem(ShopItem item)
    {
        _item = item;
        _areYouSure.SetActive(true);
        _shopMenu.SetActive(false);
    }

    public void PurchaseItem()
    {
        _item.ConfirmPurchase();
    }
}
