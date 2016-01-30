using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Serializable]
    public class RecipeElement
    {
        public Sprite Ingredient;
        public int Amount;
    }

    public RecipeElement[] Recipe;
    public Sprite[] OtherIngredients;

    public float TimeLimit;
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
}