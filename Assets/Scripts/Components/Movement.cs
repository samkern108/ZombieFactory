using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveTarget
{
	public Vector3 targetV;
	public Transform targetT;
	private bool vector = false;
	public float stoppingDistance = 0.0f;

	public MoveTarget(Vector3 target) {
		this.targetV = target;
		vector = true;
	}

	public MoveTarget(Transform target) {
		this.targetT = target;
		vector = false;
	}

	public MoveTarget(Vector3 target, float stoppingDistance) {
		this.targetV = target;
		this.stoppingDistance = stoppingDistance;
		vector = true;
	}

	public MoveTarget(Transform target, float stoppingDistance) {
		this.targetT = target;
		this.stoppingDistance = stoppingDistance;
		vector = false;
	}

	public Vector3 GetTargetPosition() {
		if (vector)
			return targetV;
		else
			return targetT.position;
	}
}

// TODO(samkern): if they reach their goal target, remove it. If there is no active target, stop moving.
public class Movement : MonoBehaviour
{
	public MovementStats stats;
	public bool moving = false;

	public MoveTarget attackTarget, goalTarget;
	public bool attackMove = false;

	public void InitializeStats (MovementStats s)
	{
		stats = s;
	}

	public void Update ()
	{
		if (moving)
			transform.position = MoveTowardTarget (attackMove ? attackTarget : goalTarget);
	}

	public void SetGoalTarget(MoveTarget target)
	{
		moving = true;
		goalTarget = target;
	}

	public void SetAttackTarget(MoveTarget target)
	{
		attackTarget = target;
		attackMove = true;
	}

	public void ClearAttackTarget()
	{
		attackMove = false;
	}

	private Vector3 MoveTowardTarget (MoveTarget target)
	{
		Vector3 stopDistance = new Vector3 ();
		if (target.stoppingDistance > 0.0f) {
			stopDistance = (target.GetTargetPosition () - transform.position).normalized * target.stoppingDistance;
		}

		return Vector3.MoveTowards(transform.position, target.GetTargetPosition() - stopDistance, 
			stats.moveSpeed * Time.deltaTime);

		/*float dist = .3f * Vector2.Distance (orders[0].target.position, transform.position);
		return AvoidCollisions (Vector2.Lerp (transform.position, target.position, 
			(Time.deltaTime * .3f * stats.moveSpeed) / dist));*/
	}

	public void PauseMovement(bool pause)
	{
		moving = !pause;
	}

	private RaycastHit2D hit;
	protected Vector3 AvoidCollisions (Vector3 position)
	{
		hit = Physics2D.Raycast (transform.position, transform.up, 5);
		Debug.DrawRay (transform.position, transform.up, Color.red, .2f, false);

		if (hit.collider != null) {
			
		}

		return position;
	}
}
