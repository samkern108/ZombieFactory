using UnityEngine;
using System.Collections;

/*
 * How movement works:
 * 
 * If a unit is "directed" when they move to their goal, they can't be stopped unless the stop order
 * carries an "interrupt direct" bool.
 * 
 * Directed movement example:
 *  - A hero being moved by the user
 * Interrupt example:
 *  - Knockback damage or something
 * Non-directed movement example:
 * 	- enemy moving to the waypoint
 * Non-direct interrupt example:
 * 	- being attacked by an enemy
 * 
 * The unit, if "stats.moving", will move toward its "target position." This is its short-term goal, such 
 * as attacking an enemy, avoiding an obstacle, or fleeing.
 * The unit will also keep track of its "goal position", which is where the unit should eventually
 * end up, such as the next waypoint (enemies) or a player-issued goal position (heroes).
 */
public class Movement : MonoBehaviour
{
	public MovementStats stats;
	public bool directed = false;

	protected Transform goal;
	public Transform target;

	public void InitializeStats (MovementStats s)
	{
		stats = s;
		goal = transform;
		target = goal;
	}

	public void Update ()
	{
		if (stats.moving) {
			if (target == null || target.position == transform.position) {
				DetermineNextTarget ();
			}
			transform.position = NextPosition ();
		}
	}

	private Vector3 NextPosition ()
	{
		if (target == null || transform == null) {
			Debug.Log (target + " " + transform + " UH OH NULL! " + transform.name + " Stopped moving.");
			return transform.position;
		}
		float dist = .3f * Vector2.Distance (target.position, transform.position);
		return AvoidCollisions (Vector2.Lerp (transform.position, target.position, 
			(Time.deltaTime * .3f * stats.moveSpeed) / dist));
	}

	protected virtual void DetermineNextTarget ()
	{
		if (directed) 
			directed = false;

		if (goal.position != target.position) {
			target = goal;
		}
		else {
			stats.moving = false;
			target = transform;
		}
	}

	//TODO - how do we want to handle 'z' position?
	public void SetGoalAndMove (Transform goal, bool direct)
	{
		if (directed && !direct)
			return;

		this.goal = goal;
		MoveToGoal (direct);
	}

	public void MoveToGoal (bool direct)
	{
		target = goal;
		stats.moving = true;
		this.directed = direct;
	}

	public void SetTemporaryTargetAndMove(Transform target, bool direct)
	{
		if (directed && !direct)
			return;

		this.target = target;
		stats.moving = true;
		this.directed = direct;
	}

	public void StopMoving (bool cancelDirect)
	{
		if (directed && cancelDirect || !directed) {
			stats.moving = false;
			this.target = transform;
			directed = false;
		}
	}

	//NOT WORKING (OBVIOUSLY)
	protected Vector3 AvoidCollisions (Vector3 position)
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position, (position - transform.position).normalized, 5);
		Debug.DrawRay (transform.position, (position - transform.position));

		return position;
	}
}
