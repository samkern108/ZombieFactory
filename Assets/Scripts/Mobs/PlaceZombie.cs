using UnityEngine;
using System.Collections;

public class PlaceZombie : MonoBehaviour {
	
	Vector3 mousePosition;
	bool placeUnit = false;
	private float speed;

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
		Vector3 newTarget;
		foreach(Zombie z in GetComponentsInChildren<Zombie>()) {
			newTarget = target + new Vector3 (Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0);
			z.gameObject.GetComponent <Movement>().SetGoalTarget(new MoveTarget(newTarget));
			z.Hibernate (false);
		}

		Destroy (this);
	}
}