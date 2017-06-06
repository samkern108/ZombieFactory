using UnityEngine;
using System.Collections;

public class MobMovement : MonoBehaviour
{
	private Vector3 startPos;
	private Vector3 target;
	private float startTime;
	private float speed;

	public void SetTarget(Vector3 target, float speed)
	{
		this.startPos = transform.position;
		this.target = target;
		this.startTime = Time.time;
		this.speed = speed;
	}

	public void Update()
	{
		if (target != null) {
			transform.position = Vector3.Lerp (startPos, target, (Time.time - startTime));
		}
	}
}
