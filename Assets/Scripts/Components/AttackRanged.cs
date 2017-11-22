using UnityEngine;
using System.Collections;

public class AttackRanged : Attack {

	public GameObject projectile;

	protected override void Damage()
	{
		Movement m = target.GetComponent<Movement>(); //TODO can we just get this ONCE when we target and store it?

		GameObject g = Instantiate (projectile);
		g.transform.position = transform.position;

		Projectile p = g.GetComponent<Projectile>();
		Vector2 fireDirection = (target.transform.position - transform.position).normalized;


		//Vector3 moveAdjust = (m.moving ? ((m.target.position - m.transform.position).normalized * m.stats.moveSpeed) : Vector3.zero);

		fireDirection = ((m.transform.position /*+ moveAdjust*/) - transform.position).normalized;

		//Debug.DrawLine (transform.position, m.transform.position, Color.green, .2f);
		//Debug.DrawLine (transform.position, moveAdjust, Color.red,.2f);
		//Debug.DrawLine (m.transform.position, m.transform.position + moveAdjust, Color.blue, .2f);

		p.Initialize (this, fireDirection);
	}
}
