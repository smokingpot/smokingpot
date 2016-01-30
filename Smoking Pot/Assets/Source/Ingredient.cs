using System;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public Rect GameArea; // for garbage collection
    public float ForceFactor;

    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!GameArea.Contains(transform.position))
        {
            Destroy(gameObject);
        }
    }

    public void Init(SpawnPoint point, Sprite sprite)
    {
        _renderer.sprite = sprite;
        transform.position = point.transform.position;
        _rigidbody.velocity = point.MinVelocity;
    }

    public void AddForce(Vector2 force)
    {
        _rigidbody.AddForce(-1.0f*ForceFactor*force);
    }

	public void AddForceAtPosition(Vector2 force, Vector2 position)
	{
		_rigidbody.AddForceAtPosition(-1.0f*ForceFactor*force, position);
	}
}