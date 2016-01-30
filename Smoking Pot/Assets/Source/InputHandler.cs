using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public enum InputMode
    {
        Impulse,
        Continuous
    }

    public InputMode CurrentInputMode;

    public float maximumDragTime = 1;
    public float maximumDragDistance = 2;
    public float forceConstant = 10;

    private const int ButtonNum = 0;

    private Ingredient _currentIngredient;
    private Vector2 _localHitPoint;
    private float _dragStartTime;

    private void Update()
    {
        switch (CurrentInputMode)
        {
            case InputMode.Impulse:
                ProcessImpulseInput();
                break;
            case InputMode.Continuous:
                ProcessContinuousInput();
                break;
        }
    }

    private void ProcessImpulseInput()
    {
        if (Input.GetMouseButtonDown(ButtonNum))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.collider != null)
            {
                Ingredient ingredient = hit.collider.gameObject.GetComponent<Ingredient>();
                if (ingredient != null)
                {
                    _localHitPoint = mouseWorldPos;
                    _currentIngredient = ingredient;
                }
            }
        }
        if (_currentIngredient != null && Input.GetMouseButtonUp(ButtonNum))
        {

            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 globalVector = _currentIngredient.transform.TransformPoint(_localHitPoint);
            Vector2 force = globalVector - mouseWorldPos;
            print(force.magnitude);
            if (force.magnitude > maximumDragDistance)
            {
                force = force * maximumDragDistance / force.magnitude;
            }
            _currentIngredient.AddForceAtPosition(force, globalVector);
            _currentIngredient = null;

        }
    }

    private void ProcessContinuousInput()
    {
        if (Input.GetMouseButtonDown(ButtonNum))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.collider != null)
            {
                Ingredient ingredient = hit.collider.gameObject.GetComponent<Ingredient>();
                if (ingredient != null)
                {
                    _currentIngredient = ingredient;
                    _dragStartTime = Time.realtimeSinceStartup;

                    _localHitPoint = ingredient.transform.InverseTransformPoint(hit.point);

                }
            }
        }

        if (Input.GetMouseButton(ButtonNum) && _currentIngredient != null && (Time.realtimeSinceStartup - _dragStartTime) < maximumDragTime)
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