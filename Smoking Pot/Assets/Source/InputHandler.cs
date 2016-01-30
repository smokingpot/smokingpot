using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const int ButtonNum = 0;

    private Ingredient _currentIngredient;
    private Vector2 _hitPoint;

    private void Update()
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
                    _hitPoint = mouseWorldPos;
                    _currentIngredient = ingredient;
                }
            }
        }
        if (_currentIngredient != null && Input.GetMouseButtonUp(ButtonNum))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 offset = _hitPoint - mouseWorldPos;
            _currentIngredient.AddForce(offset);
            _currentIngredient = null;
        }
    }
}