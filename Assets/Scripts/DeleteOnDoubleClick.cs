using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnDoubleClick : MonoBehaviour {
	private const float Delay = 0.3F;

	private bool firstClick = false;
	private float runningTimerSecond;

	public void OnMouseDown()
	{
		// If the time is too long we reset first click variable
		if (firstClick && (Time.time - runningTimerSecond) > Delay) {
			firstClick = false;
		}

		if (!firstClick) {
			firstClick = true;
			runningTimerSecond = Time.time;
		} else {
			//double click has happen
			firstClick = false;
			Destroy (gameObject);
		}
	}
}
