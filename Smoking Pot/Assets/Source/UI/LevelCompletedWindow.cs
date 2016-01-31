using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedWindow : GameWindow
{
    public Text ScoreText;

    public event Action ReplayClick;
    public event Action ExitClick;

    public void OnReplayClick()
    {
		AudioManager.Instance.playClickSound ();
        if (ReplayClick != null)
        {
            ReplayClick();
        }
    }

    public void OnExitClick()
    {
		AudioManager.Instance.playClickSound ();
        if (ExitClick != null)
        {
            ExitClick();
        }
    }

    public int Score
    {
        set
        {
            ScoreText.text = value.ToString() + "%";
        }
    }
}