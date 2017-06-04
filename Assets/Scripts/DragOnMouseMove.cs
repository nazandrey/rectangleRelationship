using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragOnMouseMove : MonoBehaviour {
	private float _cameraZPositionDelta;
	private Vector3 _offset;
	private bool _canDrag = true;

	private void OnMouseDown()
	{
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

		if (hit.collider != null && hit.collider.tag == "Rectangle") {
			_canDrag = true;
			_cameraZPositionDelta = Camera.main.WorldToScreenPoint (gameObject.transform.position).z;
			_offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _cameraZPositionDelta));
		} else {
			_canDrag = false;
		}
	}

	private void OnMouseDrag()
	{
		if (_canDrag) {
			Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _cameraZPositionDelta);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + _offset;
			transform.position = curPosition;
		}
	}
}