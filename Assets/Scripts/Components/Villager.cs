using UnityEngine;
using System.Collections;

public class Villager : Entity {

	public override void InitializeEntity()
	{
		EntityStats stats = new EntityStats ();
		stats.name = "Sam";
		stats.experienceGiven = 5;

		stats.attackStats = new AttackStats ();
		stats.attackStats.aggressive = true;
		stats.attackStats.attackDelay = 1f;
		stats.attackStats.attackWhileMoving = true;
		stats.attackStats.damage = 0.2f;
		stats.attackStats.projectile = false;
		stats.attackStats.range = 1.0f;

		stats.movementStats = new MovementStats ();
		stats.movementStats.moveSpeed = 1.0f;

		stats.healthStats = new HealthStats ();
		stats.healthStats.armor = 0.5f;
		stats.healthStats.maxHealth = 100.0f;
		stats.healthStats.regenSeconds = 1.0f;
		stats.healthStats.regenAmount = 2.0f;

		this.stats = stats;
		base.InitializeEntity ();
	}
}
