using UnityEngine;
using System.Collections;

// TODO(samkern): Replace this
public class MobMovement : MonoBehaviour
{
	private Vector3 startPos, target, moveDir;
	private float speed = 10;
	private float step = 0;

	public void SetTarget(Vector3 target, float speed)
	{
		this.startPos = transform.position;
		this.target = target;
		this.moveDir = target - startPos;
		this.speed = speed;

		foreach(Zombie z in GetComponentsInChildren<Zombie>()) {
			z.Hibernate (false);
		}
	}

	//TODO Mobs should be able to break out of the mobmovement (apply mobmovement as a force to each mob?)
	//they should be able to be intercepted by scouts, etc.
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
	}
}