using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationLineController : MonoBehaviour {
	private LineRenderer _relationLineRenderer;

	public void InitStartPoint(Vector3 position){
		_relationLineRenderer = gameObject.GetComponentInChildren<LineRenderer> ();
		_relationLineRenderer.enabled = false;
		_relationLineRenderer.SetPosition (0, position);
	}

	public void InitEndPoint(Vector3 position){
		_relationLineRenderer.SetPosition (1, position);
		_relationLineRenderer.enabled = true;
	}
}
