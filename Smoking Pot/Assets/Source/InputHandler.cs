using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float maximumDragTime = 1;
    public float maximumDragDistance = 2;
    public float forceConstant = 10;

    private const int ButtonNum = 0;

    private Ingredient _currentIngredient;
    private Vector2 _localHitPoint;
    private float _dragStartTime;
    private int _touchLayerMask;

    private void Awake()
    {
        _touchLayerMask = LayerMask.GetMask("Touch");
    }

    private void Update()
    {
        ProcessContinuousInput();
    }

    private void ProcessContinuousInput()
    {
        if (Input.GetMouseButtonDown(ButtonNum))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 100.0f, _touchLayerMask);
            if (hit.collider != null)
            {
                Ingredient ingredient = hit.collider.gameObject.GetComponentInParent<Ingredient>();
                if (ingredient != null)
                {
					ParticleSystem particleSystem = ingredient.GetComponentInChildren<ParticleSystem> ();
					ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
					emissionModule.enabled = true;

                    _currentIngredient = ingredient;
                    _dragStartTime = Time.realtimeSinceStartup;

                    _localHitPoint = ingredient.transform.InverseTransformPoint(hit.point);

                }
            }
        }

		if (Input.GetMouseButtonUp (ButtonNum)) {
			ParticleSystem particleSystem = _currentIngredient.GetComponentInChildren<ParticleSystem> ();
			ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
			emissionModule.enabled = false;
		}

        if (Input.GetMouseButton(ButtonNum) 
            && _currentIngredient != null 
            && (Time.realtimeSinceStartup - _dragStartTime) < maximumDragTime)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 globalVector = _currentIngredient.transform.TransformPoint(_localHitPoint);
            Vector2 force = globalVector - mouseWorldPos;
            if (force.magnitude > maximumDragDistance)
            {
                force = force * maximumDragDistance / force.magnitude;
            }
            _currentIngredient.AddForceAtPosition(force * forceConstant, globalVector);
        }
    }
}