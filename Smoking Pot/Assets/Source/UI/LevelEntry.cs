using System;
using UnityEngine;

public class LevelEntry : MonoBehaviour
{
    private LevelSelectionWindow _window;
    private int _levelNumber;

    public void Init(LevelSelectionWindow window, int levelNumber)
    {
        _window = window;
        _levelNumber = levelNumber;
    }

    public void OnClick()
    {
        _window.OnLevelSelected(_levelNumber);
    }
}