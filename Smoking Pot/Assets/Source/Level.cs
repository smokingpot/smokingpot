using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Sprite[] Ingredients;

    public SpawnPoint[] SpawnPoints
    {
        get { return gameObject.GetComponentsInChildren<SpawnPoint>(); }
    }
}