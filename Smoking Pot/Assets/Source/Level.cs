using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Serializable]
    public class RecipeElement
    {
        public Sprite Ingredient;
        public int AmountMin;
        public int AmountMax;

        private int _amount;

        public int Amount
        {
            get { return _amount; }
        }

        public void GenerateAmount()
        {
            _amount = UnityEngine.Random.Range(AmountMin, AmountMax + 1);
        }
    }

    public RecipeElement[] Recipe;
    public Sprite[] OtherIngredients;

    public float TimeLimit;
    public float LastIngredientTime;
    public int IngredientsCount;

    public SpawnPoint[] SpawnPoints
    {
        get { return gameObject.GetComponentsInChildren<SpawnPoint>(); }
    }

    public SpawnPoint GetRandomSpawnPoint()
    {
        SpawnPoint[] points = SpawnPoints;
        int pointNum = UnityEngine.Random.Range(0, points.Length);
        return points[pointNum];
    }

    public void Init()
    {
        foreach (var elem in Recipe)
        {
            elem.GenerateAmount();
        }
    }
}