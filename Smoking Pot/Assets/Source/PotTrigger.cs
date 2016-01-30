using System;
using UnityEngine;

public class PotTrigger : MonoBehaviour
{
	public Pot Parent;

    private void OnTriggerEnter2D(Collider2D collider)
    {
		Ingredient ingredient = collider.gameObject.GetComponent<Ingredient>();
		if (ingredient == null) 
		{
			return;
		}
		Parent.OnIngredientCaught (ingredient);
    }
}