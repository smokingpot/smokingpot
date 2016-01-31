using System;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public event Action<Ingredient> IngredientCaught;

    public void OnIngredientCaught(Ingredient ingredient)
    {
        if (IngredientCaught != null)
        {
            IngredientCaught(ingredient);
        }
    }

    public void OnEndAnimationEnded()
    {
    }
}