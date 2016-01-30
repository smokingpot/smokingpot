using System;
using UnityEngine;
using UnityEngine.UI;

public class RecipeItem : MonoBehaviour
{
    public Image ItemImage;
    public Text ItemText;

    public Sprite Image
    {
        set
        {
            ItemImage.sprite = value;
        }
    }

    public string AmountText
    {
        set
        {
            ItemText.text = value;
        }
    }
}