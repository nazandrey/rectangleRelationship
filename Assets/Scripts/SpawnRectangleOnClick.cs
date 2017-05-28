using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRectangleOnClick : MonoBehaviour {
	private const int CameraZPosition = 10;
	private const int RectangleLayerMask = 256; //2^8, where 8 - RectangleLayerNum

	public GameObject rectanglePrefab;

	public void OnMouseDown()
	{
		if (rectanglePrefab != null && NoRectanglesAround()) {
			GameObject spawnedRectangle = Instantiate (rectanglePrefab, getCameraMousePosition(), new Quaternion ());
			spawnedRectangle.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
		} else if (rectanglePrefab == null) {
			Debug.LogWarning ("no rectangle prefab for SpawnRectangleOnClick");
		}
	}

	public bool NoRectanglesAround()
	{
		SpriteRenderer rectanglePrefabRenderer = rectanglePrefab.GetComponent<SpriteRenderer>();
		Vector3 size = Vector3.zero;
		if(rectanglePrefabRenderer != null){
			size = rectanglePrefabRenderer.bounds.size;
		}
		return size != Vector3.zero ? Physics2D.OverlapBox (getCameraMousePosition (), size, 0, RectangleLayerMask) == null : false;
	}

	private Vector3 getCameraMousePosition(){
		//convert mouse position to main camera area
		Vector3 vector3MousePosition = Input.mousePosition;
		vector3MousePosition.z = CameraZPosition;
		return Camera.main.ScreenToWorldPoint(vector3MousePosition);
	}
}
