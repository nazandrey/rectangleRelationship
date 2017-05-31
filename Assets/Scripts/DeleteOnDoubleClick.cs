using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnDoubleClick : MonoBehaviour {
	private const float _Delay = 0.3F;

	private bool _firstClick = false;
	private float _runningTimerSecond;

	public void OnMouseDown()
	{
		// If the time is too long we reset first click variable
		if (_firstClick && (Time.time - _runningTimerSecond) > _Delay) {
			_firstClick = false;
		}

		if (!_firstClick) {
			_firstClick = true;
			_runningTimerSecond = Time.time;
		} else {
			//double click has happen
			_firstClick = false;
			Destroy (gameObject);
		}
	}
}
