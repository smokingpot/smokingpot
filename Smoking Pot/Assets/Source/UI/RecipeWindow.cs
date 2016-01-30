using System;

public class RecipeWindow : GameWindow
{
    public event Action PlayClick;

    public void OnPlayClick()
    {
        if (PlayClick != null)
        {
            PlayClick();
        }
    }
}
