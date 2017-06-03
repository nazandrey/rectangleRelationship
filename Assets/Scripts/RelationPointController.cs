using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationPointController : MonoBehaviour {
	private List<LineRenderer> _relationLineStartPointList = new List<LineRenderer>();
	private List<LineRenderer> _relationLineEndPointList = new List<LineRenderer>();

	public void AddRelationLinePoint(LineRenderer relationLinePoint, bool isStart){
		if (isStart) {
			_relationLineStartPointList.Add (relationLinePoint);
		} else {
			_relationLineEndPointList.Add (relationLinePoint);

		}
	}

	public void RemoveStartLine(LineRenderer relationLineRender){
		_RemoveRelationLine (relationLineRender, true);
	}

	public void RemoveEndLine(LineRenderer relationLineRender){
		_RemoveRelationLine (relationLineRender, false);
	}

	private void _UpdateRelationLinePosition(LineRenderer relationLineRender, Vector3 position, bool isStart){
		if (relationLineRender != null) {
			relationLineRender.SetPosition (isStart ? 0 : 1, position);
		}
	}

	private void _RemoveRelationLine(LineRenderer relationLineRender, bool isStart){
		if (isStart) {
			_relationLineStartPointList.Remove (relationLineRender);
		} else {
			_relationLineEndPointList.Remove (relationLineRender);
		}
	}

	private void _UpdateLinePosition(LineRenderer relationPoint, bool isStart){
		_UpdateRelationLinePosition (relationPoint, transform.position, isStart);
		relationPoint.GetComponentInChildren<RelationLineController> ().UpdateColliderPosition ();
	}

	private void Update(){
		if (transform.hasChanged) {
			foreach (LineRenderer relationLineStartPoint in _relationLineStartPointList) {
				_UpdateLinePosition (relationLineStartPoint, true);
			}
			foreach (LineRenderer relationLineEndPoint in _relationLineEndPointList) {
				_UpdateLinePosition (relationLineEndPoint, false);
			}
		}
	}

	private void OnDestroy() {
		foreach (LineRenderer relationLineStartPoint in _relationLineStartPointList) {
			Destroy (relationLineStartPoint.gameObject);
		}
		foreach (LineRenderer relationLineEndPoint in _relationLineEndPointList) {
			Destroy (relationLineEndPoint.gameObject);
		}
	}
}
