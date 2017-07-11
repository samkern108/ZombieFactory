using UnityEngine;
using System.Collections;

public class PlaceZombie : MonoBehaviour {
	
	Vector3 mousePosition;
	bool placeUnit = false;
	private float speed;

	// MobMovement and PlaceZombie should be combined
	public void Init(float speed)
	{
		this.speed = speed;
	}

	void Update () 
	{
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0;

		if (!placeUnit) transform.position = mousePosition;
		if (Input.GetMouseButtonDown (0)) {
			if (!placeUnit) placeUnit = true;
			else SetTarget (mousePosition);
		}
	}

	private void SetTarget(Vector3 target)
	{
		this.gameObject.AddComponent <MobMovement>();
		this.gameObject.GetComponent <MobMovement>().SetTarget(target, speed);

		Destroy (this);
	}
}