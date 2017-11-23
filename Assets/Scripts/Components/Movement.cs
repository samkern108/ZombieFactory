using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveTarget
{
	private Vector3 targetV;
	private Transform targetT;
	public bool vector = false;
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
		millCenter = transform.position;
	}

	public void Update ()
	{
		if (moving)
			MoveTowardTarget (attackMove ? attackTarget : goalTarget);
		else {
			MillAbout ();
		}
	}

	public void SetGoalTarget(MoveTarget target)
	{
		goalTarget = target;
		moving = true;
	}

	// TODO(samkern): Fix bug that causes units scraping by each other's radii to constantly target and untarget.
	public void SetAttackTarget(MoveTarget target)
	{
		target.stoppingDistance /= 2;
		attackTarget = target;
		attackMove = true;
		moving = true;
	}

	public void ClearAttackTarget()
	{
		if (goalTarget == null) {
			moving = false;
		}
		attackMove = false;
	}

	private void MoveTowardTarget (MoveTarget target)
	{
		Vector3 stopDistance = new Vector3 ();
		if (target.stoppingDistance > 0.0f) {
			stopDistance = (target.GetTargetPosition () - transform.position).normalized * target.stoppingDistance;
		}

		Vector3 newPosition = Vector3.MoveTowards(transform.position, target.GetTargetPosition() - stopDistance, 
			stats.moveSpeed * Time.deltaTime);

		if(Vector3.Distance((target.GetTargetPosition() - stopDistance), transform.position) < .1f) {
			if (target.vector) {
				millCenter = transform.position;
				moving = false;
			}
			return;
		}

		transform.position = newPosition;

		/*float dist = .3f * Vector2.Distance (orders[0].target.position, transform.position);
		return AvoidCollisions (Vector2.Lerp (transform.position, target.position, 
			(Time.deltaTime * .3f * stats.moveSpeed) / dist));*/
	}

	public void PauseMovement(bool pause)
	{
		moving = !pause;
	}

	public Vector3 millCenter;
	private Vector3 millTarget;
	private float millTargetWeight = 0.0f;
	private float millSpeed;
	public void MillAbout()
	{
		if (millTargetWeight < 0.0f) {
			millTargetWeight = Random.Range(.4f, 1f);
			millTarget = millCenter + new Vector3 (Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0);
			millSpeed = stats.moveSpeed * Random.Range (.1f, .35f);
		}
		millTargetWeight -= Time.deltaTime;

		transform.position = Vector3.MoveTowards(transform.position, millTarget, 
			millSpeed * Time.deltaTime);
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
