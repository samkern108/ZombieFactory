using UnityEngine;
using System.Collections;

[System.Serializable]
public class EntityStats
{
	public string uid { get; set; }
	public string name { get; set; }
	public int experienceGiven { get; set; }

	public MovementStats movementStats { get; set; }
	public AttackStats attackStats { get; set; }
	public HealthStats healthStats { get; set; }
	public VillagerStats villagerStats { get; set; }
	public int[] sfxNum { get; set; }
}

[System.Serializable]
public class MovementStats
{
	public float moveSpeed { get; set; }
}

[System.Serializable]
public class AttackStats
{
	public float damage { get; set; }
	public float attackDelay { get; set; }
	public bool aggressive { get; set; }
	public bool attackWhileMoving { get; set; }
	public bool projectile { get; set; }
	public float range { get; set; }
}

[System.Serializable]
public class HealthStats
{
	public float health { get; set; }
	public float armor { get; set; }
	public float maxHealth { get; set; }
	public float respawnTimer { get; set; }
	public bool dead = false;
	public float regenSeconds { get; set; }
	public float regenAmount { get; set; }
}

[System.Serializable]
public class VillagerStats
{
	public int level { get; set; }
}