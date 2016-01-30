using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionWindow : GameWindow
{
    public GameObject LevelEntryPrefab;

    public RectTransform Container;

    public event Action<int> LevelSelected;

    public void OnLevelSelected(int levelNumber)
    {
        if (LevelSelected != null)
        {
            LevelSelected(levelNumber);
        }
    }

    public void AddLevels(IEnumerable<Level> levels)
    {
        foreach (var level in levels)
        {
            GameObject entryObj = Instantiate(LevelEntryPrefab);
            entryObj.transform.SetParent(Container, false);
            LevelEntry entry = entryObj.GetComponent<LevelEntry>();
            entry.Init(this, level.Number);
        }
    }
}