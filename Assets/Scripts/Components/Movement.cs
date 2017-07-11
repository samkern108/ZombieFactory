using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveOrder
{
	public Transform target;
	// A priority greater than the contester will remain active.
	// A priority less than the contester will be pushed down the stack.
	public float priority;
	// If this move order is something that should be cancelled once it's popped, for example,
	// if an attack-based move order is overridden, it should not be reinstated.
	public bool interruptable;
}

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
	public bool moving = false;
	public List<MoveOrder> pendingOrders = new List<MoveOrder>(); 
	public MoveOrder activeOrder;

	// TODO(samkern):
	// Should probably create a dummy object to test move order popping,
	// directed movement, etc.

	public void InitializeStats (MovementStats s)
	{
		stats = s;
	}

	public void Update ()
	{
		if (moving && activeOrder != null) {
			if (activeOrder.target == null || activeOrder.target.position == transform.position)
				DetermineNextTarget ();
			transform.position = Move ();
		}
	}

	protected virtual void DetermineNextTarget ()
	{
		if (pendingOrders.Count > 0) {
			activeOrder = pendingOrders [0];
			pendingOrders.RemoveAt (0);
		} else {
			moving = false;
			activeOrder = null;
		}
	}

	public void AddMoveOrder(MoveOrder mo)
	{
		if (activeOrder != null && mo.priority >= activeOrder.priority) {
			InsertPendingMoveOrder (activeOrder);
			activeOrder = mo;
		} else
			InsertPendingMoveOrder (mo);
	}

	private void InsertPendingMoveOrder(MoveOrder mo) {
		for (int i = 0; i < pendingOrders.Count; i++)
			if (pendingOrders [i].priority <= mo.priority)
				pendingOrders.Insert (i, mo);
	}

	public void ClearMoveOrder(MoveOrder mo)
	{
		if (activeOrder == mo)
			DetermineNextTarget ();
		else
			pendingOrders.Remove (mo);
	}

	private Vector3 Move ()
	{
		return Vector3.MoveTowards(transform.position, activeOrder.target.position, 
			stats.moveSpeed * Time.deltaTime);

		/*float dist = .3f * Vector2.Distance (orders[0].target.position, transform.position);
		return AvoidCollisions (Vector2.Lerp (transform.position, target.position, 
			(Time.deltaTime * .3f * stats.moveSpeed) / dist));*/
	}

	public void PauseMovement()
	{
		moving = false;
	}

	public void ResumeMovement()
	{
		moving = true;
	}

	public void StopMoving ()
	{
		activeOrder = null;
		pendingOrders.Clear ();
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
