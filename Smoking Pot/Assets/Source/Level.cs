using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    public class Item
    {
        public Sprite Image;
        public int Amount;
    }

    public Sprite[] Ingredients;

    public SpawnPoint[] SpawnPoints
    {
        get { return gameObject.GetComponentsInChildren<SpawnPoint>(); }
    }
}