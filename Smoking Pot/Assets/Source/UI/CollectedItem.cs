using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectedItem : MonoBehaviour {

	public Image ItemImage;
	public Text RecipeAmount;
	public Text CollectedAmount;

	public Sprite Image
	{
		set
		{
			ItemImage.sprite = value;
		}
	}

	public string RecipeAmountText
	{
		set
		{
			RecipeAmount.text = value;
		}
	}

	public string CollectedAmountText
	{
		set
		{
			CollectedAmount.text = value;
		}
	}
}
