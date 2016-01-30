using System;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject[] Levels;
    public GameObject GamePrefab;

    public Canvas UICanvas;
    public GameObject RecipeWindowPrefab;
    public GameObject LevelCompletedWindowPrefab;

    private RecipeWindow _recipeWindow;

    private int _selectedLevelNumber;
    private Game _currentGame;

    private void Start()
    {
        _selectedLevelNumber = 0; //TODO: select from UI
        OpenRecipeWindow();
    }

    private void LoadGame()
    {
        GameObject gameObj = Instantiate(GamePrefab);
        _currentGame = gameObj.GetComponent<Game>();

        GameObject levelPrefab = Levels[_selectedLevelNumber];
        _currentGame.Begin(levelPrefab);
    }

    private void EndGame()
    {
        //TODO: destroy current game
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
        LoadGame();
    }

    #endregion
}