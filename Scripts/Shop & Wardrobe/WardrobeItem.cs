using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WardrobeItem : MonoBehaviour
{
    [HideInInspector] public ScriptableItem scriptableItem;
    [HideInInspector] public int index;

    [Header("VARIABLES")]
    [SerializeField] private Image _itemImage;
    [SerializeField] private Button _wearButton;
    [SerializeField] private TMPro.TextMeshProUGUI _text;

    private void Start()
    {
        _itemImage.sprite = scriptableItem.image;

        if (PlayerPrefs.HasKey("wardrobeItem" + index) && PlayerPrefs.GetInt("wardrobeItem" + index) == 1)
        {
            ItemAvailable();
            return;
        }

        PlayerPrefs.SetInt("wardrobeItem" + index, 0);

        ItemNotAvailable();
    }

    private void ItemNotAvailable()
    {       
        _text.text = "Not Purchased";
    }

    public void ItemAvailable()
    {
        _text.text = "Wear";
        _wearButton.interactable = true;
    }

    public void ButtonPressed()
    {
        Wardrobe.instance.ConfirmButton(scriptableItem.image, index);
    }
}
