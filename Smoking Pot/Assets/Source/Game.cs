using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject PotPrefab;
    public GameObject IngredientPrefab;

    public SpawnPoint[] TestSpawnPoints;

    private Level _level;

    public void Begin(GameObject levelPrefab)
    {
        GameObject levelObj = Instantiate(levelPrefab);
        levelObj.transform.SetParent(transform, false);
        _level = levelObj.GetComponent<Level>();

        SpawnNewIngredient(); // for test
    }

    private void Start()
    {
        GameObject potObj = Instantiate(PotPrefab);
        potObj.transform.SetParent(transform, false);

        foreach (var point in TestSpawnPoints)
        {
            point.SpawnTestIngredient();
        }
    }

    private void SpawnNewIngredient()
    {
        SpawnPoint[] points = _level.SpawnPoints;
        int pointNum = Random.Range(0, points.Length);
        SpawnPoint point = points[pointNum];

        int spriteNum = Random.Range(0, _level.Ingredients.Length);
        Sprite sprite = _level.Ingredients[spriteNum];

        GameObject ingredientObj = Instantiate(IngredientPrefab);
        ingredientObj.transform.SetParent(transform, false);
        Ingredient ingredient = ingredientObj.GetComponent<Ingredient>();
        ingredient.Init(point, sprite);
    }
}