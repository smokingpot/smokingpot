using System;
using System.Collections.Generic;

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

    public int GetResult(IEnumerable<Level.RecipeElement> recipe)
    {
        //TODO:
        int count = 0;
        foreach (var elem in recipe)
        {
            count += elem.Amount;
            if (!_progress.ContainsKey(elem.Ingredient.name))
            {
                continue;
            }
        }
        return 0;
    }
}