using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Vector2 MinVelocity;

    //TODO: random speed and angle

    public Ingredient TestIngredient;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, MinVelocity);
    }

    public void SpawnTestIngredient()
    {
        TestIngredient.Init(this);
    }
}