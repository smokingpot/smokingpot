using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
	public float maximumDragTime;
    public float maximumDragDistance;

    private const int ButtonNum = 0;

    private Ingredient _currentIngredient;
    private Vector2 _localHitPoint;
	private float _dragStartTime;

//    private void Update()
//    {
//        if (Input.GetMouseButtonDown(ButtonNum))
//        {
//            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
//            if (hit.collider != null)
//            {
//                Ingredient ingredient = hit.collider.gameObject.GetComponent<Ingredient>();
//                if (ingredient != null)
//                {
//                    _hitPoint = mouseWorldPos;
//                    _currentIngredient = ingredient;
//                }
//            }
//        }
//        if (_currentIngredient != null && Input.GetMouseButtonUp(ButtonNum))
//        {
//            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            Vector2 offset = _hitPoint - mouseWorldPos;
//            _currentIngredient.AddForce(offset);
//            _currentIngredient = null;
//        }
//    }

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
                    _currentIngredient = ingredient;
					_dragStartTime = Time.realtimeSinceStartup;

					_localHitPoint = ingredient.transform.InverseTransformPoint(hit.point);

                }
            }
        }


		if (Input.GetMouseButton (ButtonNum) && _currentIngredient != null && (Time.realtimeSinceStartup - _dragStartTime) < maximumDragTime) {
			Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 globalVector = _currentIngredient.transform.TransformPoint (_localHitPoint);
            Vector2 force = globalVector - mouseWorldPos;
            force = force * maximumDragDistance / Math.Max(force.magnitude, maximumDragDistance);
            _currentIngredient.AddForceAtPosition(force, globalVector);
		}
	}

}