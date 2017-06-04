using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationLineController : MonoBehaviour {
	private const int _ColliderWidthMultiplier = 8;

	private LineRenderer _relationLineRenderer;
	private RelationPointController _startRelationPoint;
	private RelationPointController _endRelationPoint;

	public void SaveRelationPoint(RelationPointController relationPoint, bool isStart){
		if (isStart) {
			_startRelationPoint = relationPoint;
		} else {
			_endRelationPoint = relationPoint;
		}
	}

	public void UpdateColliderPosition()
	{
		Vector2 startPosition = _relationLineRenderer.GetPosition (0);
		Vector2 endPosition = _relationLineRenderer.GetPosition (1);
		BoxCollider2D collider = (BoxCollider2D) _relationLineRenderer.GetComponentInChildren<BoxCollider2D> ();
		if (collider != null) {
			float lineLength = Vector2.Distance (startPosition, endPosition);
			// multiply collider width for better click catching
			collider.size = new Vector2 (lineLength, _relationLineRenderer.startWidth * _ColliderWidthMultiplier);
			Vector2 midPoint = (startPosition + endPosition) / 2;
			collider.transform.position = midPoint;
			// calculate the angle between startPosition and endPosition
			float angle = (Mathf.Abs (startPosition.y - endPosition.y) / Mathf.Abs (startPosition.x - endPosition.x));
			if ((startPosition.y < endPosition.y && startPosition.x > endPosition.x) || (endPosition.y < startPosition.y && endPosition.x > startPosition.x)) {
				angle *= -1;
			}
			angle = Mathf.Rad2Deg * Mathf.Atan (angle);
			collider.transform.rotation = Quaternion.Euler(0,0,angle);
		}
	}

	private void Awake(){
		_relationLineRenderer = gameObject.GetComponentInParent<LineRenderer> ();
	}

	private void OnDestroy(){
		_startRelationPoint.RemoveStartLine(_relationLineRenderer);
		_endRelationPoint.RemoveEndLine(_relationLineRenderer);
	}
}
