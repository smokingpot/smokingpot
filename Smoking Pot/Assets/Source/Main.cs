using System;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Serializable]
    public class LevelParameters
    {
        public float Time;
        public float Speed;
    }

    public LevelParameters[] Levels;

    private void Start()
    {
    }

    public void LoadLevel(LevelParameters parameters)
    {
    }
}