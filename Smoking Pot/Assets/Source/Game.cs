using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject PotPrefab;
    public GameObject IngredientPrefab;

    public SpawnPoint[] TestSpawnPoints;

    private Level _level;
	private Pot _pot;

	private Dictionary<string, int> _collectedIngredients;
    private Queue<Sprite> _ingredients;
    private Queue<float> _spawnTimeIntervals;
    private float _nextSpawnTime;

    public void Begin(GameObject levelPrefab)
    {
        GameObject levelObj = Instantiate(levelPrefab);
        levelObj.transform.SetParent(transform, false);
        _level = levelObj.GetComponent<Level>();
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        Sprite[] buffer = new Sprite[_level.IngredientsCount];
        int ind = 0;
        List<Sprite> avail = new List<Sprite>();

        foreach (Level.RecipeElement recipeElem in _level.Recipe)
        {
            avail.Add(recipeElem.Ingredient);
            for (int i = 0; i < recipeElem.Amount; i++)
            {
                buffer[ind++] = recipeElem.Ingredient;
            }
        }
        foreach (Sprite other in _level.OtherIngredients)
        {
            avail.Add(other);
        }

        while (ind < buffer.Length)
        {
            int randInd = UnityEngine.Random.Range(0, avail.Count);
            buffer[ind++] = avail[randInd];
        }

        Shuffle(buffer);
        _ingredients = new Queue<Sprite>(buffer);

        float[] spawnTimes = new float[_level.IngredientsCount];
        for (int i = 0; i < spawnTimes.Length; i++)
        {
            spawnTimes[i] = UnityEngine.Random.Range(0.0f, _level.TimeLimit);
        }
        Array.Sort(spawnTimes);

        _spawnTimeIntervals = new Queue<float>();
        float prevTime = 0.0f;
        for (int i = 0; i < spawnTimes.Length; i++)
        {
            float interval = spawnTimes[i] - prevTime;
            _spawnTimeIntervals.Enqueue(interval);
        }

        _nextSpawnTime = Time.realtimeSinceStartup + _spawnTimeIntervals.Dequeue();
    }

	private void HandleCaught(Ingredient ingredient) {
		if (!_collectedIngredients.ContainsKey(ingredient.Name)) {
			_collectedIngredients[ingredient.Name] = 0;
		}
		_collectedIngredients [ingredient.Name]++;
		Destroy (ingredient.gameObject);
	}
		
	private void Awake()
	{
		_collectedIngredients = new Dictionary<string, int> ();
	}

    private void Start()
    {
        GameObject potObj = Instantiate(PotPrefab);
        potObj.transform.SetParent(transform, false);
		_pot = potObj.GetComponent<Pot> ();
		_pot.IngredientCaught += HandleCaught;

        foreach (var point in TestSpawnPoints)
        {
            if (point != null)
            {
                point.SpawnTestIngredient();
            }
        }
    }

	private void OnDestroy() {
		_pot.IngredientCaught -= HandleCaught;
	}

    private void Update()
    {
        if (_spawnTimeIntervals == null)
        {
            return;
        }
        if (_spawnTimeIntervals.Count > 0 && Time.realtimeSinceStartup > _nextSpawnTime)
        {
            SpawnNewIngredient();
            _nextSpawnTime = Time.realtimeSinceStartup + _spawnTimeIntervals.Dequeue();
        }
    }

    private void SpawnNewIngredient()
    {
        if (_ingredients.Count == 0)
        {
            return;
        }
        Sprite sprite = _ingredients.Dequeue();
        SpawnPoint point = _level.GetRandomSpawnPoint();

        GameObject ingredientObj = Instantiate(IngredientPrefab);
        ingredientObj.transform.SetParent(transform, false);
        Ingredient ingredient = ingredientObj.GetComponent<Ingredient>();
        ingredient.Init(point, sprite);
    }

    private void Shuffle<T>(T[] arr)
    {
        int n = arr.Length;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n);
            T value = arr[k];
            arr[k] = arr[n];
            arr[n] = value;
        }
    }
}