using System;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public event Action<Ingredient> IngredientCaught;
    public event Action SummonCompleted;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnIngredientCaught(Ingredient ingredient)
    {
        if (IngredientCaught != null)
        {
            IngredientCaught(ingredient);
        }
    }

    public void OnEndAnimationEnded()
    {
        if (SummonCompleted != null)
        {
            SummonCompleted();
        }
    }

    public void StartSummonAnimation(int score)
    {
        _animator.SetInteger("SummonScore", score);
        _animator.SetTrigger("Summon");

        if (score > 90)
        {
            AudioManager.Instance.playVictorySound();
        }
        else if (score > 50)
        {
            AudioManager.Instance.playMediumVictorySound();
        }
        else
        {
            AudioManager.Instance.playDefeatSound();
        }
    }
}