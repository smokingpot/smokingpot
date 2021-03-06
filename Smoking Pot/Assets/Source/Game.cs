﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject PotPrefab;
    public GameObject IngredientPrefab;

    public SpawnPoint[] TestSpawnPoints;

    public event Action End;

    private Level _level;
	private Pot _pot;

    private Queue<Sprite> _ingredients;
    private Queue<float> _spawnTimeIntervals;
    private float _nextSpawnTime;

    private bool _running;
    private float _playTime;

    public void Create(Level level)
    {
        _level = level;
        _level.Play();
    }

    public Level CurrentLevel
    {
        get { return _level; }
    }

    public void Begin()
    {
        _pot.gameObject.SetActive(true);
        GenerateLevel();
        _running = true;
    }

    private void OnEnd()
    {
        _running = false;
        if (End != null)
        {
            End();
        }
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
            prevTime = spawnTimes[i];
        }
        if (_spawnTimeIntervals.Count > 0)
        {
            _nextSpawnTime = Time.realtimeSinceStartup + _spawnTimeIntervals.Dequeue();
        }
    }

    private void HandleCaught(Ingredient ingredient)
    {
        AudioManager.Instance.playSplashSound();
        CurrentLevel.PlayerProgress.Add(ingredient.IngredientName);
        Destroy(ingredient.gameObject);
    }

    private void HandleSummonCompleted()
    {
        OnEnd();
    }

    private void Start()
    {
        GameObject potObj = Instantiate(PotPrefab);
        potObj.transform.SetParent(transform, false);
        _pot = potObj.GetComponent<Pot>();
        _pot.gameObject.SetActive(false);

        _pot.IngredientCaught += HandleCaught;
        _pot.SummonCompleted += HandleSummonCompleted;

        foreach (var point in TestSpawnPoints)
        {
            if (point != null)
            {
                point.SpawnTestIngredient();
            }
        }
    }

    private void OnDestroy()
    {
        _pot.IngredientCaught -= HandleCaught;
        _pot.SummonCompleted -= HandleSummonCompleted;
    }

    private void Update()
    {
        if (!_running)
        {
            return;
        }

        _playTime += Time.deltaTime;
        if (_playTime > _level.TimeLimit
            && _ingredients.Count == 0
            && Ingredient.Counter == 0)
        {
            _running = false;
            _pot.StartSummonAnimation(_level.GetResult());
        }

        if (_ingredients.Count > 0 && Time.realtimeSinceStartup > _nextSpawnTime)
        {
            SpawnNewIngredient();
            if (_spawnTimeIntervals.Count > 0)
            {
                _nextSpawnTime = Time.realtimeSinceStartup + _spawnTimeIntervals.Dequeue();
            }
        }
    }

    private void SpawnNewIngredient()
    {
        Sprite sprite = _ingredients.Dequeue();
        SpawnPoint point = _level.GetRandomSpawnPoint();

        GameObject ingredientObj = Instantiate(IngredientPrefab);
        ingredientObj.transform.SetParent(transform, false);
        Ingredient ingredient = ingredientObj.GetComponent<Ingredient>();
        ingredient.Init(point, sprite);
        point.Lock();
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