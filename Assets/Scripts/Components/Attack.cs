using UnityEngine;
using System.Collections;

public abstract class Attack : MonoBehaviour {

	//TODO https://www.assetstore.unity3d.com/en/#!/content/54975 download this and switch over

	public AttackStats stats;

	public GameObject target;

	public void InitializeStats(AttackStats s)
	{
		stats = s;
		float radius = stats.range;

		GetComponent <CircleCollider2D>().radius = radius;
		// ?
		transform.Find ("AttackRadius").localScale = new Vector2(radius, radius);

		// TODO - this can probably be done upon instantiation of the object
		// if (tag.Contains ("Enemy"))
		//	gameObject.layer = LayerMask.NameToLayer ("EnemyAttack");
	}

	protected virtual void Damage(Health health)
	{
		health.TakeDamage (stats.damage,  this.transform.parent.gameObject);
	}

	public IEnumerator DamageCoroutine()
	{
		Health health = target.GetComponentInChildren<Health> ();
		GameObject myTarget = target; //race condition


		while (myTarget == target) {
			Damage (health);
			yield return new WaitForSeconds (stats.attackSpeed);
		}
	}


	/* So how does targeting work?
		 - if a unit has no target and a collider stays within their range, they target it
		 - if a unit has no target and is attacked by a viable target, they target it
		 - if a unit has a target and is attacked by a viable target, they target it if aggressive
		         otherwise, they keep their current target
		 - if a unit loses their target, they chase it if aggressive
		         otherwise, they target the closest unit. If none, they move toward goal
	*/

	private int position;

	void OnTriggerStay2D(Collider2D obj)
	{
		if (obj.gameObject == target)
			return;

		if (target == null) {
			SetTarget(obj.gameObject);
			GetComponentInParent <Movement>().StopMoving(false);
		}
	}

	//TODO - on setting a target, if this unit is "aggressive", it should also set its
	// goal position to be the target. This way, upon trigger exit, the unit will chase it.
	void OnTriggerExit2D(Collider2D obj)
	{
		if (obj.gameObject == target) {
			UnTarget ();
		}
	}

	public void AttackedBy(GameObject attacker)
	{
		if (target == null) {
			if (GetComponent<CircleCollider2D> ().OverlapPoint (attacker.transform.position)) {
				SetTarget (attacker);
			} else {
				//TODO Here is where aggressive/nonagressive should make the difference
				//Agressive units should set the attacker as a goal, not a temp target
				GetComponentInParent<Movement> ().SetTemporaryTargetAndMove (attacker.transform, false);
			}
		}
		else 
			if (target.GetComponentInChildren<Attack> ().target != this.transform.parent.gameObject)
				SetTarget (attacker);
	}

	public void SetTarget(GameObject t)
	{
		Debug.Log ("Attempting to target:  " + t);
		target = t;
		target.GetComponent<Health>().Target (this);
		//we just need to make 100% sure that we don't ever have two attacking coroutines going at once.
		StartCoroutine ("DamageCoroutine");	
	}

	public void UnTarget()
	{
		target.GetComponent<Health> ().UnTarget (this);
		ClearTarget ();
	}

	public void ClearTarget()
	{
		target = null;
		GetComponentInParent<Movement> ().MoveToGoal(false);
	}

	//TODO - it'd be safer to assign entity id numbers... 
	//Called by Health upon death
	public void TargetDead(GameObject t)
	{
		if (target == t) {
			ClearTarget ();
			/*Hero h = GetComponentInParent<Hero> ();
			if(h) 
				h.GetExperience (t.GetComponent<Enemy>().stats.experienceGiven);*/
		}
	}

	public void ShowRadius()
	{
		transform.Find ("AttackRadius").gameObject.SetActive(true);
	}

	public void HideRadius()
	{
		transform.Find ("AttackRadius").gameObject.SetActive(false);
	}
}
