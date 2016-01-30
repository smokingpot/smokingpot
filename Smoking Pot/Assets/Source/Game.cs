using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject PotPrefab;
    public GameObject IngredientPrefab;

    public SpawnPoint[] TestSpawnPoints;

    private LevelParameters _levelParameters;

    public void Begin(LevelParameters levelParameters)
    {
        _levelParameters = levelParameters;
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
        int pointNum = Random.Range(0, _levelParameters.Points.Length);
        SpawnPoint point = _levelParameters.Points[pointNum];

        int spriteNum = Random.Range(0, _levelParameters.Ingredients.Length);
        Sprite sprite = _levelParameters.Ingredients[spriteNum];

        GameObject ingredientObj = Instantiate(IngredientPrefab);
        ingredientObj.transform.SetParent(transform, false);
        Ingredient ingredient = ingredientObj.GetComponent<Ingredient>();
        ingredient.Init(point, sprite);
    }
}