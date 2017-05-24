using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRectangleOnClick : MonoBehaviour {
	//variable to take rectangle prefab
	public GameObject rectanglePrefab;

	//bind spawn prefab
	public void OnMouseDown()
	{
		//checking rectangle prefab existence
		if (rectanglePrefab != null) {
			//if found convert mouse position to main camera area
			Vector3 rectangleSpawnPosition = Input.mousePosition;
			rectangleSpawnPosition.z = 10;
			Instantiate (rectanglePrefab, Camera.main.ScreenToWorldPoint(rectangleSpawnPosition), new Quaternion ());
		} else {
			//if not found warning
			Debug.LogWarning ("no rectangle prefab for SpawnRectangleOnClick");
		}
	}
}
