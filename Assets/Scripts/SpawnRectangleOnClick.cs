using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRectangleOnClick : MonoBehaviour {
	private const int _PrefabZDelta = -1;

	private float _cameraZPositionDelta;
	private int _rectangleLayerMask;

	public GameObject rectanglePrefab;

	private bool _NoRectanglesAround()
	{
		SpriteRenderer rectanglePrefabRenderer = rectanglePrefab.GetComponent<SpriteRenderer>();
		Vector3 size = Vector3.zero;
		if(rectanglePrefabRenderer != null){
			size = rectanglePrefabRenderer.bounds.size;
		}
		return size != Vector3.zero ? Physics2D.OverlapBox (_getCameraMousePosition (), size, 0, _rectangleLayerMask) == null : false;
	}

	private Vector3 _getCameraMousePosition(){
		//convert mouse position to main camera area
		Vector3 vector3MousePosition = Input.mousePosition;
		vector3MousePosition.z = _cameraZPositionDelta + _PrefabZDelta;
		return Camera.main.ScreenToWorldPoint(vector3MousePosition);
	}

	private void Awake(){
		_rectangleLayerMask = LayerMask.GetMask("Rectangles");
		_cameraZPositionDelta = Camera.main.WorldToScreenPoint (gameObject.transform.position).z;
	}

	private void OnMouseDown()
	{
		if (rectanglePrefab != null && _NoRectanglesAround()) {
			GameObject spawnedRectangle = Instantiate (rectanglePrefab, _getCameraMousePosition(), new Quaternion ());
			spawnedRectangle.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
		} else if (rectanglePrefab == null) {
			Debug.LogWarning ("no rectangle prefab for SpawnRectangleOnClick");
		}
	}
}
