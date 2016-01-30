using System;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Init(SpawnPoint point, Sprite sprite)
    {
        _renderer.sprite = sprite;
        transform.position = point.transform.position;
        _rigidbody.velocity = point.MinVelocity;
    }
}