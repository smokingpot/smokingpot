using System;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject[] Levels;
    public GameObject GamePrefab;

    public Canvas UICanvas;
    public GameObject LevelSelectionWindowPrefab;
    public GameObject RecipeWindowPrefab;
    public GameObject LevelCompletedWindowPrefab;

    private LevelSelectionWindow _levelSelectionWindow;
    private RecipeWindow _recipeWindow;
    private LevelCompletedWindow _levelCompletedWindow;

    private List<Level> _levelInstances;
    private int _selectedLevelNumber;
    private Game _currentGame;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        _levelInstances = new List<Level>();
    }

    private void Start()
    {
        for (int num = 0; num < Levels.Length; num++)
        {
            var levelPrefab = Levels[num];
            GameObject levelObj = Instantiate(levelPrefab);
            levelObj.transform.SetParent(transform, false);
            Level level = levelObj.GetComponent<Level>();
            level.Init(num);
            _levelInstances.Add(level);
        }

        OpenLevelSelectionWindow();
		AudioManager.Instance.playMenuMusic ();
    }

    private void LoadGame()
    {
        GameObject gameObj = Instantiate(GamePrefab);
        _currentGame = gameObj.GetComponent<Game>();

        Level level = _levelInstances[_selectedLevelNumber];
        _currentGame.Create(level);

        _currentGame.End += HandleGameEnd;
    }

    private void HandleGameEnd()
    {
        _currentGame.End -= HandleGameEnd;
        Destroy(_currentGame.gameObject);
        _currentGame = null;

        OpenLevelCompletedWindow();
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

    #region Recipe Window

    private void OpenRecipeWindow()
    {
        if (_recipeWindow != null)
        {
            throw new InvalidOperationException("Recipe window already opened");
        }

        _recipeWindow = OpenWindow<RecipeWindow>(RecipeWindowPrefab);
        _recipeWindow.ShowRecipe(_currentGame.CurrentLevel.Recipe);
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
        _currentGame.Begin();
		AudioManager.Instance.playGameMusic ();
    }

    #endregion

    #region Level Completed Window

    private void OpenLevelCompletedWindow()
    {
        _levelCompletedWindow = OpenWindow<LevelCompletedWindow>(LevelCompletedWindowPrefab);
    }

    private void CloseLevelCompletedWindow()
    {
        CloseWindow(ref _levelCompletedWindow);
    }

    #endregion

    #region Level Selection Window

    private void OpenLevelSelectionWindow()
    {
        _levelSelectionWindow = OpenWindow<LevelSelectionWindow>(LevelSelectionWindowPrefab);
        _levelSelectionWindow.LevelSelected += HandleLevelSelected;
        _levelSelectionWindow.AddLevels(_levelInstances);
    }

    private void CloseLevelSelectionWindow()
    {
        _levelSelectionWindow.LevelSelected -= HandleLevelSelected;
        CloseWindow(ref _levelSelectionWindow);
    }

    private void HandleLevelSelected(int levelNumber)
    {
        _selectedLevelNumber = levelNumber;
        CloseLevelSelectionWindow();

        LoadGame();
        OpenRecipeWindow();
    }

    #endregion
}