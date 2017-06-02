using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationPointController : MonoBehaviour {
	private List<LineRenderer> _relationLineStartPointList = new List<LineRenderer>();
	private List<LineRenderer> _relationLineEndPointList = new List<LineRenderer>();

	private void _UpdateRelationLinePosition(LineRenderer relationLineRender, Vector3 position, bool isStart){
		if (relationLineRender != null) {
			relationLineRender.SetPosition (isStart ? 0 : 1, position);
		}
	}

	public void AddRelationLineStartPoint(LineRenderer relationLinePoint){
		_relationLineStartPointList.Add (relationLinePoint);
	}

	public void AddRelationLineEndPoint(LineRenderer relationLinePoint){
		_relationLineEndPointList.Add (relationLinePoint);
	}

	public void UpdateRelationLineStartPosition(LineRenderer relationLineRender, Vector3 position){
		_UpdateRelationLinePosition (relationLineRender, position, true);
	}

	public void UpdateRelationLineEndPosition(LineRenderer relationLineRender, Vector3 position){
		_UpdateRelationLinePosition (relationLineRender, position, false);
	}

	private void Update(){
		if (transform.hasChanged) {
			foreach (LineRenderer relationLineStartPoint in _relationLineStartPointList) {
				UpdateRelationLineStartPosition (relationLineStartPoint, transform.position);
				relationLineStartPoint.GetComponentInChildren<RelationLineController> ().UpdateColliderPosition ();
			}

			foreach (LineRenderer relationLineEndPoint in _relationLineEndPointList) {
				UpdateRelationLineEndPosition (relationLineEndPoint, transform.position);
				relationLineEndPoint.GetComponentInChildren<RelationLineController> ().UpdateColliderPosition ();
			}
		}
	}

	private void OnDestroy() {
		foreach (LineRenderer relationLineStartPoint in _relationLineStartPointList) {
			Destroy (relationLineStartPoint);
		}
		foreach (LineRenderer relationLineEndPoint in _relationLineEndPointList) {
			Destroy (relationLineEndPoint);
		}
	}
}
