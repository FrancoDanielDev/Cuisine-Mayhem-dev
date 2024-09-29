using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wardrobe : MonoBehaviour
{
    public static Wardrobe instance;

    [HideInInspector] public WardrobeItem[] wardrobeItem;

    [SerializeField] private WardrobeItem _wardrobeItemPrefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private Outfits _outfits;
    [SerializeField] private GameObject _confirmButton;
    [SerializeField] private Image _confirmItemImage;

    private int _index;

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
        wardrobeItem = new WardrobeItem[_outfits.item.Length];

        for (int i = 0; i < _outfits.item.Length; i++)
        {
            var newButton = Instantiate(_wardrobeItemPrefab, _parent);
            newButton.scriptableItem = _outfits.item[i];
            newButton.index = i;

            wardrobeItem[i] = newButton;
        }
    }

    public void ConfirmButton(Sprite sprite, int index)
    {
        _confirmButton.SetActive(true);
        _confirmItemImage.sprite = sprite;
        PlayerPrefs.SetInt("hat" + index, 0);
        _index = index;
    }

    public void ButtonPressed()
    {
        for (int i = 0; i < wardrobeItem.Length; i++)
        {
            if (i == _index)
            {
                PlayerPrefs.SetInt("hat" + i, 1);
                continue;
            }

            PlayerPrefs.SetInt("hat" + i, 0);
            _confirmButton.SetActive(false);
        }
    }
}
