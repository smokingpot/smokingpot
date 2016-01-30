using System;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Serializable]
    public class LevelParameters
    {
        public float Time;
        public float Speed;
        public SpawnPoint[] Points;
    }

    public LevelParameters[] Levels;

    public Canvas UICanvas;

    public GameObject RecipeWindowPrefab;
    public GameObject LevelCompletedWindowPrefab;

    private RecipeWindow _recipeWindow;

    private void Start()
    {
        OpenRecipeWindow();
    }

    private void LoadLevel(LevelParameters parameters)
    {
    }

    private T OpenWindow<T>(GameObject prefab) where T : GameWindow
    {
        GameObject windowObj = Instantiate(prefab);
        windowObj.transform.SetParent(UICanvas.transform, false);
        return windowObj.GetComponent<T>();
    }

    private void CloseWindow<T>(ref T window) where T : GameWindow
    {
        Destroy(window.gameObject);
        window = null;
    }

    #region Recipe Screen

    private void OpenRecipeWindow()
    {
        if (_recipeWindow != null)
        {
            throw new InvalidOperationException("Recipe window already opened");
        }

        _recipeWindow = OpenWindow<RecipeWindow>(RecipeWindowPrefab);
        _recipeWindow.PlayClick += HandlePlayClick;
    }

    private void CloseRecipeWindow()
    {
        if (_recipeWindow == null)
        {
            throw new InvalidOperationException("Recipe window is not opened");
        }

        _recipeWindow.PlayClick -= HandlePlayClick;
        CloseWindow(ref _recipeWindow);
    }

    private void HandlePlayClick()
    {
        CloseRecipeWindow();
    }

    #endregion
}