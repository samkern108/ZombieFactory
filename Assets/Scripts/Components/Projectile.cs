using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	//play sound on awake?

	private bool attackEnemies = true;
	protected bool homing = false;

	private float damage = 7f, speed = 3f;

	private AttackRanged callback;

	private Vector3 fireDirection;

	public void OnTriggerEnter2D(Collider2D obj)
	{
		if (obj.transform.GetComponent<Health> () == null)
			return;

		//maybe if it hits a wall destroy self?

		Health damaged = obj.transform.GetComponent<Health> ();
		if (damaged != null)
			damaged.TakeDamage (damage, callback.transform.parent.gameObject);
		Destroy (this.gameObject);
	}

	public void Initialize(AttackRanged callback, Vector2 fireDirection)
	{
		this.callback = callback;
		this.fireDirection = fireDirection;

		float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 180));

		if (callback.transform.parent.tag.Contains ("Enemy"))
			this.gameObject.layer = LayerMask.NameToLayer ("EnemyProjectile");
	}

	public void Update()
	{
		transform.position += fireDirection * speed * Time.deltaTime;
	}

	void OnBecameInvisible() {
		Destroy (this.gameObject);
	}
}
