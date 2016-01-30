using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelEntry : MonoBehaviour
{
    public Text NumberText;

    private LevelSelectionWindow _window;
    private int _levelNumber;

    public void Init(LevelSelectionWindow window, int levelNumber)
    {
        _window = window;
        _levelNumber = levelNumber;
        NumberText.text = (_levelNumber + 1).ToString();
    }

    public void OnClick()
    {
        _window.OnLevelSelected(_levelNumber);
    }
}