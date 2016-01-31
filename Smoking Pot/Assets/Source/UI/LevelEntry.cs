using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelEntry : MonoBehaviour
{
    public Text NumberText;
    public Text ScoreText;

    private LevelSelectionWindow _window;
    private int _levelNumber;

    public void Init(LevelSelectionWindow window, int levelNumber, int score)
    {
        _window = window;
        _levelNumber = levelNumber;
        NumberText.text = (_levelNumber + 1).ToString();
        ScoreText.text = score.ToString() + "%";
    }

    public void OnClick()
    {
		AudioManager.Instance.playClickSound ();
        _window.OnLevelSelected(_levelNumber);
    }
}