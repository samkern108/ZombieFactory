using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	public EntityStats stats;

	public virtual void InitializeEntity(EntityStats u)
	{
		stats = u;

		GetComponentInChildren <Movement>().InitializeStats(u.movementStats);
		GetComponentInChildren <Attack>().InitializeStats(u.attackStats);
		GetComponentInChildren <Health>().InitializeStats(u.healthStats);
	}

	public virtual void Die()
	{
		Destroy (this.gameObject);
	}
}
