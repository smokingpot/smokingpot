using System;
using UnityEngine;

public class LogoWindow : GameWindow
{
    public event Action Close;

    public void OnClose()
    {
        if (Close != null)
        {
            Close();
        }
    }
}