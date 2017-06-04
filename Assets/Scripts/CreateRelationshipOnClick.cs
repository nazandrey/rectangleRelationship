using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRelationshipOnClick : MonoBehaviour {
	[SerializeField]private GameObject relationLinePrefab;

	private bool _creatingRelationship = false;
	private RelationLineController _relationLineController;
	private SpriteRenderer _startRelationPointRenderer;
	private Transform _startRelationPoint;

	private void _InitRelationPoint(Transform relationPoint, LineRenderer relationLineRenderer, RelationLineController relationLineController, bool isStart){
		RelationPointController relationPointController = relationPoint.GetComponent<RelationPointController> ();
		Vector3 position = relationPoint.position;

		relationLineRenderer.SetPosition (isStart ? 0 : 1, position);
		relationLineController.SaveRelationPoint (relationPointController, isStart);
		relationPointController.SaveRelationLine (relationLineRenderer, isStart);
	}

	private void _CancelCreating(){
		_startRelationPointRenderer.color = Color.white;
		_startRelationPointRenderer = null;
		_startRelationPoint = null;
		_creatingRelationship = false;
	}

	private void Update (){
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

			//handle only if we click exactly on visible part of RelationPoint
			if (hit.collider != null && hit.collider.tag == "RelationPoint") {
				//init creating relationship, save start point
				if (!_creatingRelationship) {
					_creatingRelationship = true;
					_startRelationPointRenderer = hit.collider.GetComponent<SpriteRenderer> ();
					_startRelationPointRenderer.color = Color.black;
					_startRelationPoint = hit.collider.transform;
				} else {
					Transform endRelationPoint = hit.collider.transform;

					//if second click is not on the same rectangle create relationship
					if (_startRelationPoint.parent != endRelationPoint.parent) {
						GameObject relationLine = Instantiate (relationLinePrefab);
						LineRenderer relationLineRenderer = relationLine.GetComponent<LineRenderer> ();
						RelationLineController relationLineController = relationLine.GetComponentInChildren<RelationLineController> ();

						_InitRelationPoint (_startRelationPoint, relationLineRenderer, relationLineController, true);
						_InitRelationPoint (endRelationPoint, relationLineRenderer, relationLineController, false);
						relationLineController.UpdateColliderPosition ();
					}

					_CancelCreating ();
				}
			} else if(_creatingRelationship){
				//if second click has not hit relation point just cancel creating
				_CancelCreating ();
			}
		}
	}
}
