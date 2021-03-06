﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Attack : MonoBehaviour {

	//TODO https://www.assetstore.unity3d.com/en/#!/content/54975 download this and switch over

	public AttackStats stats;

	public GameObject target;
	protected bool haveTarget = false;

	private List<GameObject> queuedTargets = new List<GameObject> ();

	private float lastAttackTimestamp;
	private float retargetTime = 1.0f;

	private Movement movement;

	public void InitializeStats(AttackStats s)
	{
		stats = s;
		float radius = stats.range;

		GetComponent <CircleCollider2D>().radius = radius;

		movement = GetComponentInParent <Movement>();
		// ?
		//transform.Find ("AttackRadius").localScale = new Vector2(radius, radius);

		// TODO - this can probably be done upon instantiation of the object
		// if (tag.Contains ("Enemy"))
		//	gameObject.layer = LayerMask.NameToLayer ("EnemyAttack");
	}

	protected virtual void Damage()
	{
		// TODO - Store Health
		target.GetComponent<Health>().TakeDamage (stats.damage, this.transform.parent.gameObject);
		lastAttackTimestamp = Time.time;
	}

	public void StartDamageCoroutine()
	{
		StartCoroutine ("DamageCoroutine");
	}

	private IEnumerator DamageCoroutine()
	{
		// 
		while(haveTarget) {
			Damage ();
			yield return new WaitForSeconds(stats.attackDelay);
		}
	}

	public void AcquireTarget(GameObject newTarget) {
		if (!haveTarget) {
			target = newTarget;
			movement.SetAttackTarget (new MoveTarget(target.transform, stats.range));
			haveTarget = true;
			// Don't start attacking until a proper amount of time has passed.
			// TODO(samkern): Ensure this is working
			Invoke ("StartDamageCoroutine", (retargetTime - (Time.time - lastAttackTimestamp)));
		} else
			queuedTargets.Add (newTarget);
	}

	private bool Retarget()
	{
		if (queuedTargets.Count == 0) {
			haveTarget = false;
			movement.ClearAttackTarget();
			return false;
		}
		target = queuedTargets [0];
		movement.SetAttackTarget (new MoveTarget(target.transform, stats.range));
		queuedTargets.RemoveAt (0);
		return true;
	}

	public void TargetDead(GameObject deadTarget)
	{
		if (target == deadTarget)
			Retarget ();
	}
		
	protected void OnTriggerEnter2D(Collider2D obj)
	{
		GameObject enemy = obj.gameObject;
		AcquireTarget (enemy);
	}

	void OnTriggerExit2D(Collider2D obj)
	{
		queuedTargets.Remove (obj.gameObject);
		if (obj.gameObject == target)
			target = null;
		Retarget ();
	}

	public void AttackedBy(GameObject attacker)
	{
		//AcquireTarget(attacker);
		/*if (target == null) {
			if (GetComponent<CircleCollider2D> ().OverlapPoint (attacker.transform.position)) {
				SetTarget (attacker);
			} else {
				//TODO Here is where aggressive/nonagressive should make the difference
				//Agressive units should set the attacker as a goal, not a temp target
				attackMoveOrder.priority = 1.0f;
				attackMoveOrder.target = attacker.transform;
				GetComponentInParent<Movement> ().AddMoveOrder(attackMoveOrder);
			}
		}
		else 
			if (target.GetComponentInChildren<Attack> ().target != this.transform.parent.gameObject)
				SetTarget (attacker);*/
	}

	// TODO(samkern): These are defunct.
	public void ShowRadius()
	{
		transform.Find ("AttackRadius").gameObject.SetActive(true);
	}

	public void HideRadius()
	{
		transform.Find ("AttackRadius").gameObject.SetActive(false);
	}
}
