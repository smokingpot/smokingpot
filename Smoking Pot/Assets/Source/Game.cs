using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject PotPrefab;
    public GameObject IngredientPrefab;

    private Pot _pot;
    private LevelParameters _levelParameters;

    private int _score;

    public int Score
    {
        get { return _score; }
    }

    public void Begin(LevelParameters levelParameters)
    {
        _levelParameters = levelParameters;
        SpawnNewIngredient(); // for test
    }

    private void Start()
    {
        GameObject potObj = Instantiate(PotPrefab);
        potObj.transform.SetParent(transform, false);
        _pot = potObj.GetComponent<Pot>();
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