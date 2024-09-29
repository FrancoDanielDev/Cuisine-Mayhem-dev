using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [HideInInspector] public ScriptableItem scriptableItem;
    [HideInInspector] public int index;

    [Header("VARIABLES")]
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMPro.TextMeshProUGUI _itemText;
    [SerializeField] private TMPro.TextMeshProUGUI _buyText;
    [SerializeField] private Button _buyButton;

    private void Start()
    {
        Initially();
    }

    private void Initially()
    {
        if (PlayerPrefs.HasKey("shopItem" + index) && PlayerPrefs.GetInt("shopItem" + index) == 1)
        {
            PurchasedItem();
            return;
        }

        PlayerPrefs.SetInt("shopItem" + index, 0);

        BasicSettings("Buy (" + (-scriptableItem.cost) + ")");
    }

    public void ButtonPressed()
    {
        if (SaveWithPlayerPrefs.instance.currency >= scriptableItem.cost)
        {
            Shop.instance.SaveAPurchasableItem(this);
        }
    }

    public void ConfirmPurchase()
    {
        SaveWithPlayerPrefs.instance.currency -= scriptableItem.cost;
        Shop.instance.AlterCurrencyText();
        PurchasedItem();
        AudioManager.instance.Play("Cash");
    }

    private void PurchasedItem()
    {
        BasicSettings("Purchased");
        _itemText.fontStyle = TMPro.FontStyles.Italic;
        _itemText.fontStyle = TMPro.FontStyles.Strikethrough;
        _buyButton.interactable = false;

        PlayerPrefs.SetInt("shopItem" + index, 1);
        PlayerPrefs.SetInt("wardrobeItem" + index, 1);

        Wardrobe.instance.wardrobeItem[index].ItemAvailable();
    }

    private void BasicSettings(string text)
    {
        _itemImage.sprite = scriptableItem.image;
        _itemText.text = scriptableItem.name;
        _buyText.text = text;
    }
}
