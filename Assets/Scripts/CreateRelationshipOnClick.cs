using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRelationshipOnClick : MonoBehaviour {
	public GameObject relationLinePrefab;

	private bool _creatingRelationship = false;
	private GameObject _relationLine;
	private RelationLineController _relationLineController;
	private SpriteRenderer _startingRelationPointRenderer;

	private void _AddRelationLinePoint(Collider2D relationPoint, bool isStart){
		RelationPointController relationLinePointStorage = relationPoint.GetComponent<RelationPointController> ();
		LineRenderer relationLine = _relationLine.GetComponentInChildren<LineRenderer> ();
		if (isStart) {
			relationLinePointStorage.AddRelationLineStartPoint (relationLine);
		} else {
			relationLinePointStorage.AddRelationLineEndPoint (relationLine);
		}
	}

	private void _AddRelationLineStartPoint(Collider2D relationPoint){
		_AddRelationLinePoint (relationPoint, true);
	}

	private void _AddRelationLineEndPoint(Collider2D relationPoint){
		_AddRelationLinePoint (relationPoint, false);
	}

	private void Update (){
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

			if (hit.collider != null && hit.collider.tag == "RelationPoint") {
				if (!_creatingRelationship) {
					_creatingRelationship = true;
					_startingRelationPointRenderer = hit.collider.GetComponent<SpriteRenderer> ();
					_startingRelationPointRenderer.color = Color.black;

					_relationLine = Instantiate(relationLinePrefab);
					_relationLineController = _relationLine.GetComponentInChildren<RelationLineController> ();
					_relationLineController.InitStartPoint (hit.collider.transform.position);
					_AddRelationLineStartPoint (hit.collider);
				} else {
					_relationLineController.InitEndPoint (hit.collider.transform.position);
					_AddRelationLineEndPoint (hit.collider);

					_startingRelationPointRenderer.color = Color.white;
					_creatingRelationship = false;
				}
			}
		}
	}
}
