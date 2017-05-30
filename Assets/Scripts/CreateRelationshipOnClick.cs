using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRelationshipOnClick : MonoBehaviour {
	public GameObject relationLineContainerPrefab;
	private bool _creatingRelationship = false;
	private RelationLineController _relationLineController;

	void Update (){
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

			if (hit.collider != null && hit.collider.tag == "RelationPoint") {
				if (!_creatingRelationship) {
					_creatingRelationship = true;
					GameObject relationLine = Instantiate(relationLineContainerPrefab);
					_relationLineController = relationLine.GetComponent<RelationLineController> ();
					_relationLineController.InitStartPoint (hit.collider.transform.position);
				} else {
					_relationLineController.InitEndPoint (hit.collider.transform.position);
					_creatingRelationship = false;
				}
			}
		}
	}
}
