using System;
using System.Collections.Generic;
using UnityEngine;

public class Progress
{
    private readonly Dictionary<string, int> _progress = new Dictionary<string, int>();

    public void Reset()
    {
        _progress.Clear();
    }

    public void Add(string ingredientName)
    {
        if (!_progress.ContainsKey(ingredientName))
        {
            _progress[ingredientName] = 0;
        }
        _progress[ingredientName]++;
    }

    public int GetResult(Level.RecipeElement[] recipe)
    {
        int count = 0;
        int good = 0;
        int bad = 0;
        foreach (var elem in recipe)
        {
            count += elem.Amount;
            if (!_progress.ContainsKey(elem.Ingredient.name))
            {
                continue;
            }
            int val = _progress[elem.Ingredient.name];
            if (val > elem.Amount)
            {
                good += elem.Amount;
                bad += (val - elem.Amount);
            }
            else
            {
                good += val;
            }
        }
        if (count == 0)
        {
            return 0;
        }
        foreach (var collected in _progress)
        {
            if (!Array.Exists(recipe, elem => elem.Ingredient.name == collected.Key))
            {
                bad += collected.Value;
            }
        }
        float result = Mathf.Max(good - bad, 0) / (float)count;
        return (int)(result * 100.0f);
    }
}