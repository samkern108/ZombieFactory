using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Attack : MonoBehaviour {

	//TODO https://www.assetstore.unity3d.com/en/#!/content/54975 download this and switch over

	public AttackStats stats;

	public GameObject target;
	protected bool haveTarget = false;

	private List<GameObject> queuedTargets = new List<GameObject> ();

	private MoveOrder attackMoveOrder;

	public void InitializeStats(AttackStats s)
	{
		stats = s;
		float radius = stats.range;

		GetComponent <CircleCollider2D>().radius = radius;

		attackMoveOrder = new MoveOrder ();
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
	}

	public IEnumerator DamageCoroutine()
	{
		while(haveTarget) {
			Damage ();
			Debug.Log ("Damage Coroutine");
			yield return new WaitForSeconds(stats.attackDelay);
		}
	}

	public void AcquireTarget(GameObject newTarget) {
		if (!haveTarget) {
			target = newTarget;
			haveTarget = true;
			Debug.Log ("Starting coroutine");
			StartCoroutine ("DamageCoroutine");
		} else
			queuedTargets.Add (newTarget);
	}

	private bool Retarget()
	{
		if (queuedTargets.Count == 0) {
			haveTarget = false;
			return false;
		}

		target = queuedTargets [0];
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
		AcquireTarget(attacker);
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
