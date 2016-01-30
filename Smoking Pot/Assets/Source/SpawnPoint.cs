using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public float SpeedMin;
    public float SpeedMax;
    public float RandomAngle;
    public float AngularSpeedMin;
    public float AngularSpeedMax;

    public Ingredient TestIngredient;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position,
            Quaternion.AngleAxis(RandomAngle * 0.5f, Vector3.forward)
            * transform.right.normalized * SpeedMax);
        Gizmos.DrawRay(transform.position,
            Quaternion.AngleAxis(-RandomAngle * 0.5f, Vector3.forward)
            * transform.right.normalized * SpeedMax);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position,
            Quaternion.AngleAxis(RandomAngle * 0.5f, Vector3.forward)
            * transform.right.normalized * SpeedMin);
        Gizmos.DrawRay(transform.position, 
            Quaternion.AngleAxis(-RandomAngle * 0.5f, Vector3.forward) 
            * transform.right.normalized * SpeedMin);
    }

    public void SpawnTestIngredient()
    {
        TestIngredient.Init(this);
    }

    public Vector2 GetRandomVelocity()
    {
        float speed = UnityEngine.Random.Range(SpeedMin, SpeedMax);
        float angle = UnityEngine.Random.Range(0.0f, RandomAngle);
        return Quaternion.AngleAxis(angle, Vector3.forward) 
            * transform.right.normalized * speed;
    }

    public float GetRandomAngularSpeed()
    {
        return UnityEngine.Random.Range(AngularSpeedMin, AngularSpeedMax);
    }
}