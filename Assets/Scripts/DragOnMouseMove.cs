using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragOnMouseMove : MonoBehaviour {
	private Vector3 _screenPoint;
	private Vector3 _offset;
	private bool _canDrag = true;

	public void OnMouseDown()
	{
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

		if (hit.collider != null && hit.collider.tag == "Rectangle") {
			_canDrag = true;
			_screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
			_offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
		} else {
			_canDrag = false;
		}
	}

	public void OnMouseDrag()
	{
		if (_canDrag) {
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + _offset;
			transform.position = curPosition;
		}
	}
}