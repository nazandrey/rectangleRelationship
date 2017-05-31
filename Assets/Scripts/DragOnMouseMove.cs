using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragOnMouseMove : MonoBehaviour {
	private Vector3 screenPoint;
	private Vector3 offset;
	private bool canDrag = true;

	public void OnMouseDown()
	{
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

		if (hit.collider != null && hit.collider.tag == "Rectangle") {
			canDrag = true;
			screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		} else {
			canDrag = false;
		}
	}

	public void OnMouseDrag()
	{
		if (canDrag) {
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
			transform.position = curPosition;
		}
	}
}