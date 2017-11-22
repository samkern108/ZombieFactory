using UnityEngine;
using System.Collections;

public class Zombie : Entity {
	
	public void SetMyStats(float h, float a, float p, float at, float s)
	{
		InitializeEntity (h, a, p, at, s);
	}

	public void InitializeEntity(float health, float armor, float plague, float attack, float speed)
	{
		EntityStats stats = new EntityStats ();
		stats.name = "Zombie Jim";
		stats.experienceGiven = 5;

		stats.attackStats = new AttackStats ();
		stats.attackStats.aggressive = true;
		stats.attackStats.attackDelay = 0.5f;
		stats.attackStats.attackWhileMoving = false;
		stats.attackStats.damage = attack;
		stats.attackStats.projectile = false;
		stats.attackStats.range = 1.0f;

		stats.movementStats = new MovementStats ();
		stats.movementStats.moveSpeed = speed;

		stats.healthStats = new HealthStats ();
		stats.healthStats.armor = armor;
		stats.healthStats.maxHealth = health;
		stats.healthStats.regenSeconds = 1.0f;
		stats.healthStats.regenAmount = 2.0f;

		this.stats = stats;
		InitializeEntity ();
	}

	public override void InitializeEntity ()
	{
		base.InitializeEntity ();
	}
}
