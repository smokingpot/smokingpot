using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedWindow : GameWindow
{
    public event Action ReplayClick;
    public event Action ExitClick;

    public void OnReplayClick()
    {
        if (ReplayClick != null)
        {
            ReplayClick();
        }
    }

    public void OnExitClick()
    {
    }
}