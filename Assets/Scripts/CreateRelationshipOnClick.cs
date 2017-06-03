using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRelationshipOnClick : MonoBehaviour {
	public GameObject relationLinePrefab;

	private bool _creatingRelationship = false;
	private RelationLineController _relationLineController;
	private SpriteRenderer _startRelationPointRenderer;
	private Transform _startRelationPoint;

	private void _InitRelationPoint(Transform relationPoint, LineRenderer relationLineRenderer, RelationLineController relationLineController, bool isStart){
		RelationPointController relationPointController = relationPoint.GetComponent<RelationPointController> ();
		Vector3 position = relationPoint.position;

		relationLineRenderer.SetPosition (isStart ? 0 : 1, position);
		relationLineController.SaveRelationPoint (relationPointController, isStart);
		relationPointController.AddRelationLinePoint (relationLineRenderer, isStart);
	}

	private void Update (){
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

			if (hit.collider != null && hit.collider.tag == "RelationPoint") {
				if (!_creatingRelationship) {
					_creatingRelationship = true;
					_startRelationPointRenderer = hit.collider.GetComponent<SpriteRenderer> ();
					_startRelationPointRenderer.color = Color.black;
					_startRelationPoint = hit.collider.transform;
				} else {
					GameObject relationLine = Instantiate(relationLinePrefab);
					LineRenderer relationLineRenderer = relationLine.GetComponent<LineRenderer> ();
					Transform endRelationPoint = hit.collider.transform;
					RelationLineController relationLineController = relationLine.GetComponentInChildren<RelationLineController> ();

					_InitRelationPoint (_startRelationPoint, relationLineRenderer, relationLineController, true);
					_InitRelationPoint (endRelationPoint, relationLineRenderer, relationLineController, false);

					relationLineController.UpdateColliderPosition ();

					_startRelationPointRenderer.color = Color.white;
					_creatingRelationship = false;
				}
			}
		}
	}
}
