using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public float SpeedMin;
    public float SpeedMax;
    public float RandomAngle;
    public float AngularSpeedMin;
    public float AngularSpeedMax;
    public float LockTime = 1.0f;

    public Ingredient TestIngredient;

    private float _lastLockTime;

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
        float angle = UnityEngine.Random.Range(-RandomAngle * 0.5f, RandomAngle * 0.5f);
        return Quaternion.AngleAxis(angle, Vector3.forward) 
            * transform.right.normalized * speed;
    }

    public float GetRandomAngularSpeed()
    {
        return UnityEngine.Random.Range(AngularSpeedMin, AngularSpeedMax);
    }

    public void Lock()
    {
        _lastLockTime = Time.realtimeSinceStartup;
    }

    public bool IsLocked
    {
        get { return (Time.realtimeSinceStartup - _lastLockTime) < LockTime; }
    }
}