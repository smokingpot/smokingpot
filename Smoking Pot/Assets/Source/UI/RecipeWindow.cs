using System;
using System.Collections.Generic;
using UnityEngine;

public class RecipeWindow : GameWindow
{
    public GameObject ItemPrefab;
    public RectTransform Container;

    public event Action PlayClick;

    public void OnPlayClick()
    {
		AudioManager.Instance.playClickSound ();
        if (PlayClick != null)
        {
            PlayClick();
        }
    }

    public void ShowRecipe(IEnumerable<Level.RecipeElement> recipe)
    {
        foreach (var elem in recipe)
        {
            GameObject itemObj = Instantiate(ItemPrefab);
            RecipeItem item = itemObj.GetComponent<RecipeItem>();
            item.transform.SetParent(Container, false);
            item.Image = elem.Ingredient;
            item.AmountText = elem.Amount.ToString();
        }
    }
}
