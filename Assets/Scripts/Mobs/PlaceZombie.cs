using UnityEngine;
using System.Collections;

public class PlaceZombie : MonoBehaviour {
	
	Vector3 mousePosition;
	bool mouseDown = false;
	bool placeUnit = false;

	void Update () 
	{
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0;
		mouseDown = Input.GetMouseButtonDown (0);

		if (!placeUnit) {
			transform.position = mousePosition;
			if (mouseDown) {
				placeUnit = true;
			}
		} else {
			if (mouseDown) {
				PlaceUnit (mousePosition);
			}
		}
	}

	private void PlaceUnit(Vector3 target)
	{
		this.gameObject.AddComponent <MobMovement>();
		this.gameObject.GetComponent <MobMovement>().SetTarget(target, 1.0f);
		Destroy (this);
	}
}