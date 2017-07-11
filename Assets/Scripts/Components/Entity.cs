using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	public EntityStats stats;

	public virtual void InitializeEntity() {
		GetComponentInChildren <Movement>().InitializeStats(stats.movementStats);
		GetComponentInChildren <Attack>().InitializeStats(stats.attackStats);
		GetComponentInChildren <Health>().InitializeStats(stats.healthStats);
	}

	public virtual void Die()
	{
		Destroy (this.gameObject);
	}
}
