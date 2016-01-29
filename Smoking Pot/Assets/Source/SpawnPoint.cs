using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Vector2 MinVelocity;
    //TODO: random speed and angle

	private void Start()
	{
	}

	private void Update()
	{
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, MinVelocity);
    }
}