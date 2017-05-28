using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRectangleOnClick : MonoBehaviour {
	public GameObject rectanglePrefab;
	private const int CameraZPosition = 10;

	public void OnMouseDown()
	{
		if (rectanglePrefab != null) {
			//convert mouse position to main camera area
			Vector3 rectangleSpawnPosition = Input.mousePosition;
			rectangleSpawnPosition.z = CameraZPosition;
			Instantiate (rectanglePrefab, Camera.main.ScreenToWorldPoint(rectangleSpawnPosition), new Quaternion ());
		} else {
			Debug.LogWarning ("no rectangle prefab for SpawnRectangleOnClick");
		}
	}
}
