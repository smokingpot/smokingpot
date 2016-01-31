using System;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public Rect GameArea; // for garbage collection
    public float ForceFactor;

    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody;

    private static int _counter; // how many items alive

    public static int Counter
    {
        get { return _counter; }
    }

    private void Awake()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _counter++;
    }

    private void OnDestroy()
    {
        _counter--;
    }

    private void Start()
    {
        gameObject.AddComponent<PolygonCollider2D>();
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
        Init(point);
    }

    public void Init(SpawnPoint point)
    {
        _rigidbody.velocity = point.GetRandomVelocity();
        _rigidbody.angularVelocity = point.GetRandomAngularSpeed();
    }

    public void AddForce(Vector2 force)
    {
        _rigidbody.AddForce(-1.0f * force);
    }

    public void AddForceAtPosition(Vector2 force, Vector2 position)
    {
        _rigidbody.AddForceAtPosition(-1.0f * force, position);
    }

    public string IngredientName
    {
        get { return _renderer.sprite.name; }
    }
}